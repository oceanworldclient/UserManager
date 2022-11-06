using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class RestoreBoughtEvent : LogEvent<RestoreBoughtEventPayload>
{
}

public class RestoreBoughtEventPayload
{
    public Shop Shop { get; set; }

    public UserDto BeforeBought { get; set; }

    public UserDto AfterBought { get; set; }
}