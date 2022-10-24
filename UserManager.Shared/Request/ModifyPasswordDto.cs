namespace UserManager.Shared;

public class ModifyPasswordDto:BaseDto
{
    public int Id { get; set; }

    public string NewPassword { get; set; }
    
}