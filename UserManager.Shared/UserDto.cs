namespace UserManager.Shared;

public class UserDto
{
    public int Id { get; set; }
    
    public string UserName { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public long? Phone { get; set; }
    
    public DateTime ClassExpire { get; set; }
    
    public int Class { get; set; }
    
    public int NodeGroup { get; set; }
    
    public int T { get; set; }
    
    public long U { get; set; }
    
    public long D { get; set; }

    public Website Website { get; set; }

}