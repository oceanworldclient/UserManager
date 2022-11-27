using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using UserManager.Razor.State;

namespace UserManager.Blazor;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthState _authState;

    public ApiAuthenticationStateProvider(AuthState authState, ILocalStorageService localStorage)
    {
        _authState = authState;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>(AuthState.AccessToken);
        var refreshToken = await _localStorage.GetItemAsync<string>(AuthState.RefreshToken);

        if (string.IsNullOrWhiteSpace(savedToken))
        {
            MarkUserAsLoggedOut();
            return new AuthenticationState(new ClaimsPrincipal());
        }
        var authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        if (IsExpire(authenticationState, refreshToken))
        {
            MarkUserAsLoggedOut();
            return new AuthenticationState(new ClaimsPrincipal());
        }
        _authState.Authorization = new AuthenticationHeaderValue(AuthState.AuthScheme, savedToken);
        _authState.AuthenticationState = authenticationState;
        return authenticationState;
    }

    private static bool IsExpire(AuthenticationState authenticationState, string refreshToken)
    {
        var user = authenticationState.User;
        var exp = user!.FindFirst(c => c.Type.Equals("exp"))!.Value;
        var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
        var timeUtc = DateTime.UtcNow;
        var diff = expTime - timeUtc;
        if (diff.TotalSeconds < 0) return true;
        var strings = refreshToken.Split(".");
        if (strings.Length != 2) return true;
        var t = long.Parse(strings[1]);
        var now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        return now > t;
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
        _authState.Authorization = new AuthenticationHeaderValue(AuthState.AuthScheme, token);
        _authState.AuthenticationState = authState.Result;
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
        _authState.Authorization = null;
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs!.TryGetValue(ClaimTypes.Role, out var roles);

        if (roles != null)
        {
            if (roles!.ToString()!.Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles!.ToString()!);

                claims.AddRange(parsedRoles!.Select(parsedRole => new Claim(ClaimTypes.Role, parsedRole)));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles!.ToString()!));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}