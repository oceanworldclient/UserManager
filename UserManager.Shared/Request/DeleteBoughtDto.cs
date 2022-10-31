namespace UserManager.Shared.Request;

public class DeleteBoughtDto : BaseDto
{
    public long Id { get; set; }
    
    public int UserId { get; set; }
    
    public string UserEmail { get; set; }

}