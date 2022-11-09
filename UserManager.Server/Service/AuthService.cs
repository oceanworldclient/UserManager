using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserManager.Server.Entity;
using UserManager.Server.Utils;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Service;

public sealed class AuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private IdentityUser? _user;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterModel registerModel)
    {
        var user = new IdentityUser() { UserName = registerModel.Name, Email = registerModel.Email };
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        return result;
    }

    public async Task<bool> ValidateUserAsync(LoginModel loginDto)
    {
        _user = await _userManager.FindByEmailAsync(loginDto.Email);
        var result = _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
        return result;
    }

    public async Task<string> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _user!.UserName),
            new(JwtRegisteredClaimNames.Email, _user.Email),
        };
        var roles = await _userManager.GetRolesAsync(_user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    public string GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken()
        {
            Token = RandomString(),
            Timestamp = DateTime.Now.AddHours(16).Timestamp(),
            UserEmail = _user!.Email
        };
        var result = JsonSerializer.Serialize(refreshToken).Encrypt();
        return result + "." + refreshToken.Timestamp;
    }

    public async Task<TokenDto> FetchToken(string refreshToken, string accessToken)
    {
        var refresh = JsonSerializer.Deserialize<RefreshToken>(refreshToken.Split(".")[0].Decrypt());
        if (refresh == null) return new TokenDto();
        var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);
        var claims = claimsPrincipal.Claims;
        try
        {
            var first = claims.First(it => it.Type.Contains("email"));
            var email = first.Value;
            if (email != refresh.UserEmail) return new TokenDto();
            _user = await _userManager.FindByEmailAsync(email);
            if (refresh.Timestamp - DateTime.Now.Timestamp() < 3600)
            {
                refreshToken = GenerateRefreshToken();
            }

            return new TokenDto() { IsSuccess = true, token = await CreateTokenAsync(), refreshToken = refreshToken };
        }
        catch
        {
            return new TokenDto();
        }
    }
    

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
            ValidateLifetime = false,
            ValidIssuer = jwtSettings["ValidIssuer"],
            ValidAudience = jwtSettings["ValidAudience"],
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private static string RandomString()
    {
        var random = new Random();
        var length = random.Next(16, 32);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}