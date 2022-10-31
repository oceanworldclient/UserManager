using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class ModifyPasswordEvent : LogEvent<ModifyPasswordPayload>
{
}

public class ModifyPasswordPayload
{
    
    public UserBaseInfoDto UserBaseInfo { get; set; }

}