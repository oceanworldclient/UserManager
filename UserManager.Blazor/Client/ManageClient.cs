using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using UserManager.Blazor.Services;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Blazor.Client;

public class ManageClient
{
    private HttpClient HttpClient { get; }

    private IAuthService AuthService { get; }

    private static string UserController => "User";

    private static string ShopController => "Shop";

    private static string BoughtController => "Bought";

    public ManageClient(HttpClient client, IAuthService authService)
    {
        HttpClient = client;
        AuthService = authService;
    }

    public async Task<IList<UserDto>> FindUser(QueryUserDto userDto)
    {
        var res = await PostAsJson($"{UserController}/FindUser", userDto,
            JsonContext.Default.ListUserDto);
        return res ?? new List<UserDto>();
    }

    public async Task<bool> SaveUser(UserDto userDto)
    {
        var resp = await PostAsJson($"{UserController}/ModifyUser", userDto,
            JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        var resp = await PostAsJson($"{UserController}/ModifyPassword", dto,
            JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }

    public async Task<BaseResult> ModifyRefBy(ModifyRefByDto dto)
    {
        var resp = await PostAsJson($"{UserController}/ModifyRefBy", dto,
            JsonContext.Default.BaseResult);
        return resp ?? new BaseResult() { IsSuccess = false, Message = "后台异常" };
    }

    public async Task<IList<BoughtDto>> FindBoughtByUser(int userId)
    {
        var resp = await PostAsJson($"{BoughtController}/FindShop", new QueryBoughtDto() { UserId = userId },
            JsonContext.Default.ListBoughtDto);
        return resp ?? new List<BoughtDto>();
    }

    private async Task<T?> PostAsJson<T>(string uri, object dto, JsonTypeInfo<T> jsonTypeInfo)
        where T : class, new()
    {
        HttpClient.DefaultRequestHeaders.Authorization = await ((AuthService)AuthService).GetAuthHeader();
        var resp = await HttpClient.PostAsJsonAsync(uri, dto);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync(jsonTypeInfo);
    }

    public async Task<bool> DeleteBought(DeleteBoughtDto dto)
    {
        var res = await PostAsJson($"{BoughtController}/DeleteBought", dto, JsonContext.Default.BaseResult);
        return res?.IsSuccess ?? false;
    }

    public async Task<bool> CloseRenew(CloseRenewDto dto)
    {
        var res = await PostAsJson($"{BoughtController}/CloseRenew", dto, JsonContext.Default.BaseResult);
        return res?.IsSuccess ?? false;
    }

    public async Task<IList<BoughtDto>> QueryBoughtByUserId(QueryBoughtDto queryBoughtDto)
    {
        var res = await PostAsJson($"{BoughtController}/QueryBoughtByUserId", queryBoughtDto,
            JsonContext.Default.ListBoughtDto);
        return res ?? new List<BoughtDto>();
    }

    public async Task<BaseResult> BuyShop(BuyShopDto buyShopDto)
    {
        return (await PostAsJson($"{BoughtController}/BuyShop", buyShopDto, JsonContext.Default.BaseResult)) ??
               new BaseResult() { IsSuccess = false, Message = "后端服务异常" };
    }

    public async Task<BaseResult> Upgrade(BuyShopDto buyShopDto)
    {
        return (await PostAsJson($"{BoughtController}/Upgrade", buyShopDto, JsonContext.Default.BaseResult)) ??
               new BaseResult() { IsSuccess = false, Message = "后端服务异常" };
    }

    public async Task<IList<ShopDto>> QueryShop(QueryShopDto queryShopDto)
    {
        var res = await PostAsJson($"{ShopController}/QueryShop", queryShopDto, JsonContext.Default.ListShopDto);
        return res ?? new List<ShopDto>();
    }

    public async Task<BaseResult> RestoreBought(RestoreBoughtDto restoreBoughtDto)
    {
        return (await PostAsJson($"{BoughtController}/RestoreBought", restoreBoughtDto,
            JsonContext.Default.BaseResult)) ?? new BaseResult() { IsSuccess = false, Message = "后端服务异常" };
    }
}