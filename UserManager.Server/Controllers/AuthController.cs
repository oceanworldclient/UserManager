using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    private readonly UserManager<IdentityUser> _userManager;
    
    public AuthController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel login)
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
        if (user == null)
        {
            return Ok(new LoginResult() { Successful = false, Error = "用户不存在" });
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, false, false);

        if (!result.Succeeded) return BadRequest(new LoginResult() { Successful = false, Error = "用户名或者密码错误" });
        
        var roles = await _signInManager.UserManager.GetRolesAsync(user);

        var claims = roles.Select(it => new Claim(ClaimTypes.Role, it)).ToList();
        claims.Insert(0, new Claim(ClaimTypes.Name, login.Email)); 

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddHours(int.Parse(_configuration["JwtExpiryInHours"]));

        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );
        return Ok(new LoginResult() { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var newUser = new IdentityUser() { UserName = model.Name, Email = model.Email };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            return BadRequest(new RegisterResult() { Successful = false, Errors = errors });
        }

        var role = Role.Empty.ToString();
        await _userManager.AddToRoleAsync(newUser, role);
        return Ok(new RegisterResult() { Successful = true });
    }
    
}