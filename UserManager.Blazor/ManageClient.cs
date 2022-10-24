using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Blazor;

public class ManageClient
{
    private HttpClient HttpClient { get; }

    public HttpRequestHeaders DefaultRequestHeaders => HttpClient.DefaultRequestHeaders;

    private static string UserController => "UserController";

    private static string ShopController => "ShopController";

    private static string BoughtController => "BoughtController";

    private static string AuthController => "AuthController";

    private static readonly Dictionary<string, JsonTypeInfo> JsonTypeInfos = new()
    {
        { nameof(UserDto), JsonContext.Default.UserDto },
        { nameof(ShopDto), JsonContext.Default.ShopDto },
        { nameof(BoughtDto), JsonContext.Default.BoughtDto }
    };

    public ManageClient(HttpClient client)
    {
        HttpClient = client;
    }

    public async Task<IList<UserDto>> FindUser(QueryUserDto userDto)
    {
        var res = await PostAsJson<List<UserDto>>($"{UserController}/FindUser", userDto);
        return res ?? new List<UserDto>();
    }

    private async Task<T?> PostAsJson<T>(string uri, object dto) where T : class, new()
    {
        var resp = await HttpClient.PostAsJsonAsync(uri, dto);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<T>();
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var res = await PostAsJson<LoginResult>($"{AuthController}/Login", loginModel);
        return res ?? new LoginResult() { Successful = false };
    }

    public async Task<RegisterResult> Register(RegisterModel registerModel)
    {
        var res = await PostAsJson<RegisterResult>($"{AuthController}/Login", registerModel);
        return res ?? new RegisterResult() { Successful = false };
    }
}