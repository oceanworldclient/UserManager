using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class UpgradeShopEvent : LogEvent<UpgradeShopPayload>
{
}

public class UpgradeShopPayload
{
    
    public User User { get; set; }

    public Shop OldShop { get; set; }

    public Shop NewShop { get; set; }
}