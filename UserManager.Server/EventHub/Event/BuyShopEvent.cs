using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class BuyShopEvent:AbsentEvent<BuyShopEventPayload>
{
    
}

public class BuyShopEventPayload
{
    public UserDto UserDto { get; set; }
    
    public Shop Shop { get; set; }
    
}