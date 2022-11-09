namespace UserManager.Server.Entity;

public class RefreshToken
{
    
    public long Timestamp { get; set; }

    public string Token { get; set; } = "";

    public string UserEmail { get; set; } = "";

}