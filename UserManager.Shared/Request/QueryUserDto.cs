namespace UserManager.Shared;

public class QueryUserDto: BaseDto
{
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public int Id { get; set; }
    
    public string Contact { get; set; }
    
    public QueryType Type { get; set; }
        
    public enum QueryType
    {
        Id, Email, Contact
    }
    
}
