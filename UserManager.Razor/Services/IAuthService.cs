using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Razor.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
    Task<RegisterResult> Register(RegisterModel registerModel);
}