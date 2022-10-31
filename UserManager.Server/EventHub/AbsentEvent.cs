namespace UserManager.Server.EventHub;

public abstract class AbsentEvent<T>
{
    
    public string Operator { get; set; }
    
    public string UserEmail { get; set; }
    
    public T Payload { get; set; }
    
}