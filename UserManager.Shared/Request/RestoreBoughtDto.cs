namespace UserManager.Shared.Request;

public class RestoreBoughtDto : BaseDto
{
    public string UserEmail { get; set; } = "";

    public int UserId { get; set; }
}