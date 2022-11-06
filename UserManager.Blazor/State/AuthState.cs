using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace UserManager.Blazor.State;

public class AuthState
{
    public AuthenticationHeaderValue? Authorization { get; set; } = null;
    
    public AuthenticationState AuthenticationState { get; set; }

    public const string AccessToken = "UserManagerToken";

    public const string RefreshToken = "UserManagerRefreshToken";

    public const string AuthScheme = "bearer";

    public bool IsExpire()
    {
        return CalRestExpireTime().TotalSeconds < 0;
    }

    private TimeSpan CalRestExpireTime()
    {
        var user = AuthenticationState.User;
        var exp = user!.FindFirst(c => c.Type.Equals("exp"))!.Value;
        var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
        var timeUtc = DateTime.UtcNow;
        return expTime - timeUtc;
    }

    public bool NeedRefreshToken()
    {
        return CalRestExpireTime().TotalMinutes < 2;
    }
    
}