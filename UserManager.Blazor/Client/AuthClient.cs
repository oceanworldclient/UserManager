using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Blazor.Client;

public class AuthClient
{
    private HttpClient HttpClient { get; }
    
    private static string AuthController => "Auth";
    
    public AuthClient(HttpClient client)
    {
        HttpClient = client;
    }
    
    public async Task<TokenDto> RefreshToken(FetchTokenDto dto)
    {
        var res = await PostAsJson($"{AuthController}/RefreshToken", dto, JsonContext.Default.TokenDto);
        return res ?? new TokenDto() { IsSuccess = false };
    }
    
    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var res = await PostAsJson($"{AuthController}/Login", loginModel, JsonContext.Default.LoginResult);
        return res ?? new LoginResult() { Successful = false };
    }

    public async Task<RegisterResult> Register(RegisterModel registerModel)
    {
        var res = await PostAsJson($"{AuthController}/Register", registerModel,
            JsonContext.Default.RegisterResult);
        return res ?? new RegisterResult() { Successful = false };
    }
    
    private async Task<T?> PostAsJson<T>(string uri, object dto, JsonTypeInfo<T> jsonTypeInfo)
        where T : class, new()
    {
        var resp = await HttpClient.PostAsJsonAsync(uri, dto);
        if (!resp.IsSuccessStatusCode) return null;
        var x = resp.Content.ReadAsStringAsync();
        return await resp.Content.ReadFromJsonAsync(jsonTypeInfo);
    }

}