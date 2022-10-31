namespace UserManager.Shared.Request;

public class CloseRenewDto : BaseDto
{
    public long Id { get; set; }
    
    public int UserId { get; set; }
    
    public string UserEmail { get; set; }
    
}