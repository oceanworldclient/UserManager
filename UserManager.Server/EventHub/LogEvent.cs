using UserManager.Server.Utils;
using UserManager.Shared;
using UserManager.Shared.Enum;

namespace UserManager.Server.EventHub;

public class LogEvent<T> : AbsentEvent<T>
{
    
    public Website Website { get; set; }
    
    protected LogEvent()
    {
        Operator = AppHttpContext.Operator;
    }

}