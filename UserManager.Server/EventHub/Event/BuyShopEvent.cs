using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class BuyShopEvent : LogEvent<BuyShopEventPayload>
{
}

public class BuyShopEventPayload
{
    public User User { get; set; }

    public Shop Shop { get; set; }
}