using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class IllegalOperationEvent : LogEvent<IllegalOperationPayload>
{
}

public class IllegalOperationPayload
{
    public UserBaseInfoDto UserBaseInfo { get; set; }
    public string Content { get; set; }
    
}