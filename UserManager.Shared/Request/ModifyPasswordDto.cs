namespace UserManager.Shared;

public class ModifyPasswordDto : BaseDto
{
    public int UserId { get; set; }

    public string UserMail { get; set; } = "";
    public string NewPassword { get; set; } = "";
    
}