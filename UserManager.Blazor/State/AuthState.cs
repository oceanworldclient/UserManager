using System.Net.Http.Headers;

namespace UserManager.Blazor.State;

public class AuthState
{
    public AuthenticationHeaderValue? Authorization { get; set; } = null;
}