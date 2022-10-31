using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class CloseRenewEvent : LogEvent<CloseRenewPayload>
{
}

public class CloseRenewPayload
{
    public UserBaseInfoDto UserBaseInfo { get; set; }

    public ShopDto Shop { get; set; }
}