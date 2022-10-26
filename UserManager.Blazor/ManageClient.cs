using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Blazor;

public class ManageClient
{
    private HttpClient HttpClient { get; }

    public HttpRequestHeaders DefaultRequestHeaders => HttpClient.DefaultRequestHeaders;

    private static string UserController => "User";

    private static string ShopController => "Shop";

    private static string BoughtController => "Bought";

    private static string AuthController => "Auth";

    private static readonly Dictionary<string, JsonTypeInfo> JsonTypeInfos = new()
    {
        { nameof(UserDto), JsonContext.Default.UserDto },
        { nameof(ShopDto), JsonContext.Default.ShopDto },
        { nameof(BoughtDto), JsonContext.Default.BoughtDto },
        { "list" + nameof(UserDto), JsonContext.Default.ListUserDto }
    };

    public ManageClient(HttpClient client)
    {
        HttpClient = client;
    }

    public async Task<IList<UserDto>> FindUser(QueryUserDto userDto)
    {
        var res = await PostAsJson<List<UserDto>>($"{UserController}/FindUser", userDto, JsonContext.Default.ListUserDto);
        return res ?? new List<UserDto>();
    }

    public async Task<bool> SaveUser(UserDto userDto)
    {
        var resp = await PostAsJson<BaseResult>($"{UserController}/UpdateUser", userDto, JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        var resp = await PostAsJson<BaseResult>($"{UserController}/ModifyPassword", dto, JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }

    private async Task<T?> PostAsJson<T>(string uri, object dto, JsonTypeInfo<T> jsonTypeInfo)
        where T : class, new()
    {
        var resp = await HttpClient.PostAsJsonAsync(uri, dto);
        if (!resp.IsSuccessStatusCode) return null;
        var x = resp.Content.ReadAsStringAsync();
        return await resp.Content.ReadFromJsonAsync(jsonTypeInfo);
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var res = await PostAsJson<LoginResult>($"{AuthController}/Login", loginModel, JsonContext.Default.LoginResult);
        return res ?? new LoginResult() { Successful = false };
    }

    public async Task<RegisterResult> Register(RegisterModel registerModel)
    {
        var res = await PostAsJson<RegisterResult>($"{AuthController}/Register", registerModel, JsonContext.Default.RegisterResult);
        return res ?? new RegisterResult() { Successful = false };
    }
}