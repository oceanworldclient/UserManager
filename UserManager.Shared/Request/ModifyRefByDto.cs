namespace UserManager.Shared.Request;

public class ModifyRefByDto : BaseDto
{
    public int UserId { get; set; }

    public string UserEmail { get; set; } = "";

    public string RefBy { get; set; } = "";
}