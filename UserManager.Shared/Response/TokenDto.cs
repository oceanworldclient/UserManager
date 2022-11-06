namespace UserManager.Shared.Response;

public class TokenDto : BaseResult
{
    public string token { get; set; }

    public string refreshToken { get; set; }
}