using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using UserManager.Blazor;
using UserManager.Razor.Client;
using UserManager.Razor.State;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Razor.Services;

public class AuthService : IAuthService
{
    private readonly AuthClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthState _authState;

    public AuthService(AuthClient httpClient,
        AuthState authState,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
        _authState = authState;
    }

    public async Task<AuthenticationHeaderValue> GetAuthHeader()
    {
        if (!_authState.NeedRefreshToken()) return _authState.Authorization!;
        var token = await FetchToken();
        if (!string.IsNullOrEmpty(token))
            _authState.Authorization = new AuthenticationHeaderValue(AuthState.AuthScheme, token);
        return _authState.Authorization!;
    }

    private async Task<string> FetchToken()
    {
        var accessToken = await _localStorage.GetItemAsync<string>(AuthState.AccessToken);
        var refreshToken = await _localStorage.GetItemAsync<string>(AuthState.RefreshToken);
        var tokenDto = await _httpClient.RefreshToken(new FetchTokenDto()
            { token = accessToken, refreshToken = refreshToken });
        if (!tokenDto.IsSuccess) return "";
        _ = _localStorage.SetItemAsync(AuthState.AccessToken, tokenDto.token);
        _ = _localStorage.SetItemAsync(AuthState.RefreshToken, tokenDto.refreshToken);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(tokenDto.token);
        return tokenDto.token;
    }

    public async Task<RegisterResult> Register(RegisterModel registerModel)
    {
        return await _httpClient.Register(registerModel);
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var result = await _httpClient.Login(loginModel);
        if (!result.Successful) return result;
        
        await _localStorage.SetItemAsync(AuthState.AccessToken, result.Token);
        await _localStorage.SetItemAsync(AuthState.RefreshToken, result.RefreshToken);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Token);
        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(AuthState.AccessToken);
        await _localStorage.RemoveItemAsync(AuthState.RefreshToken);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
    }
}