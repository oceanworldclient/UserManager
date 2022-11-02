﻿using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using UserManager.Blazor.State;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Blazor;

public class ManageClient
{
    private HttpClient HttpClient { get; }

    private static string UserController => "User";

    private static string ShopController => "Shop";

    private static string BoughtController => "Bought";

    private static string AuthController => "Auth";

    // private static readonly Dictionary<string, JsonTypeInfo> JsonTypeInfos = new()
    // {
    //     { nameof(UserDto), JsonContext.Default.UserDto },
    //     { nameof(ShopDto), JsonContext.Default.ShopDto },
    //     { nameof(BoughtDto), JsonContext.Default.BoughtDto },
    //     { "list" + nameof(UserDto), JsonContext.Default.ListUserDto }
    // };

    public ManageClient(HttpClient client, AuthState authState)
    {
        HttpClient = client;
        HttpClient.DefaultRequestHeaders.Authorization = authState.Authorization;
        // AuthState = authState;
    }
    

    public async Task<IList<UserDto>> FindUser(QueryUserDto userDto)
    {
        var res = await PostAsJson($"{UserController}/FindUser", userDto,
            JsonContext.Default.ListUserDto);
        return res ?? new List<UserDto>();
    }

    public async Task<bool> SaveUser(UserDto userDto)
    {
        var resp = await PostAsJson($"{UserController}/UpdateUser", userDto,
            JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        var resp = await PostAsJson<BaseResult>($"{UserController}/ModifyPassword", dto,
            JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
    }
    
    public async Task<bool> ModifyRefBy(ModifyRefByDto dto)
    {
        var resp = await PostAsJson<BaseResult>($"{UserController}/ModifyRefBy", dto,
            JsonContext.Default.BaseResult);
        return resp?.IsSuccess ?? false;
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
        var resp = await HttpClient.PostAsJsonAsync(uri, dto);
        if (!resp.IsSuccessStatusCode) return null;
        var x = resp.Content.ReadAsStringAsync();
        return await resp.Content.ReadFromJsonAsync(jsonTypeInfo);
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
        var res = await PostAsJson($"{BoughtController}/QueryBoughtByUserId", queryBoughtDto, JsonContext.Default.ListBoughtDto);
        return res ?? new List<BoughtDto>();
    }
    
    public async Task<BaseResult> BuyShop(BuyShopDto buyShopDto)
    {
        return (await PostAsJson($"{BoughtController}/BuyShop", buyShopDto, JsonContext.Default.BaseResult))??new BaseResult(){IsSuccess = false, Message = "后端服务异常"};
    }
    
    public async Task<BaseResult> Upgrade(BuyShopDto buyShopDto)
    {
        return (await PostAsJson($"{BoughtController}/Upgrade", buyShopDto, JsonContext.Default.BaseResult))??new BaseResult(){IsSuccess = false, Message = "后端服务异常"};
    }
    
    public async Task<IList<ShopDto>> QueryShop(QueryShopDto queryShopDto)
    {
        var res = await PostAsJson($"{ShopController}/QueryShop", queryShopDto, JsonContext.Default.ListShopDto);
        return res ?? new List<ShopDto>();
    }
    
}