using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class UpgradeShopEvent : LogEvent<UpgradeShopPayload>
{
}

public class UpgradeShopPayload
{
    
    public Shop OldShop { get; set; }

    public Shop NewShop { get; set; }
    
    public UserDto BeforeBought { get; set; }
    
    public UserDto AfterBought { get; set; }
    
}