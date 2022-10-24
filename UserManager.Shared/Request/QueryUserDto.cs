namespace UserManager.Shared;

public class QueryUserDto: BaseDto
{
    public string UserName { get; set; } = "";

    public string Email { get; set; } = "";

    public int Id { get; set; } = 0;

    public string Contact { get; set; } = "";

    public QueryType Type { get; set; } = QueryType.Email;
        
    public enum QueryType
    {
        Id, Email, Contact, Username
    }
    
}
