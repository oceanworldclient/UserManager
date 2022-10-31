namespace UserManager.Shared;

public class ModifyPasswordDto : BaseDto
{
    public int UserId { get; set; }
    
    public string UserEmail { get; set; }
    public string NewPassword { get; set; } = "";
    
}