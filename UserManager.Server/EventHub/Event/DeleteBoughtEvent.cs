using UserManager.Server.Model;
using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Server.EventHub.Event;

public class DeleteBoughtEvent:LogEvent<DeleteBoughtPayload>
{
    
}

public class DeleteBoughtPayload
{
    public UserBaseInfoDto UserBaseInfo { get; set; }
    
    public Shop Shop { get; set; }
}