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
    public async Task<IActionResult> Login(LoginModel login)
    {
        var result = await AuthService.ValidateUserAsync(login);
        if (result) return Ok(new LoginResult() { Token = await AuthService.CreateTokenAsync(), Successful = true });
        return Ok(new LoginResult() { Successful = false });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var result = await AuthService.RegisterUserAsync(model);
        if (result.Succeeded) return Ok(new RegisterResult() { Successful = true });
        return Ok(new RegisterResult() { Successful = false });
    }
}