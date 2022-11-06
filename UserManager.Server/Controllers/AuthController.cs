using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private AuthService AuthService { get; }
    
    public AuthController(AuthService authService)
    {
        AuthService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var result = await AuthService.ValidateUserAsync(login);
        if (!result) return Ok(new LoginResult() { Successful = false });
        var token = await AuthService.CreateTokenAsync();
        var refreshToken = AuthService.GenerateRefreshToken();
        return Ok(new LoginResult() { Token = token, RefreshToken = refreshToken, Successful = true });
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] FetchTokenDto fetchTokenDto)
    {
        return Ok(await AuthService.FetchToken(fetchTokenDto.refreshToken, fetchTokenDto.token));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await AuthService.RegisterUserAsync(model);
        if (result.Succeeded) return Ok(new RegisterResult() { Successful = true });
        return Ok(new RegisterResult() { Successful = false });
    }
}