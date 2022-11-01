using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class ModifyUserEvent : LogEvent<IList<UserDto>>
{
}