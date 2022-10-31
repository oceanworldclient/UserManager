using UserManager.Server.Model;

namespace UserManager.Server.EventHub;

public interface IEvent
{
    public string Operator { get; set; }
    
    public string UserEmail { get; set; }
    
    public object Payload { get; set; }
}