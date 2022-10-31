using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class IllegalOperationEvent:AbsentEvent<UserDto>
{
    
}

public class IllegalOperationPayload
{

    public string Operation { get; set; }

}