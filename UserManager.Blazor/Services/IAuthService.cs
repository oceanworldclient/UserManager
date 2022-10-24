using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Blazor.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
    Task<RegisterResult> Register(RegisterModel registerModel);
}