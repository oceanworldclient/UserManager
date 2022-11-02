using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class BuyShopEvent : LogEvent<BuyShopEventPayload>
{
}

public class BuyShopEventPayload
{
    public Shop Shop { get; set; }
    
    public UserDto BeforeBought { get; set; }
    
    public UserDto AfterBought { get; set; }
    
}