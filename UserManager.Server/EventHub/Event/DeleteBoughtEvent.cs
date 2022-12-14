using UserManager.Shared;

namespace UserManager.Server.EventHub.Event;

public class DeleteBoughtEvent : LogEvent<DeleteBoughtPayload>
{
}

public class DeleteBoughtPayload
{
    public UserBaseInfoDto UserBaseInfo { get; set; }

    public ShopDto Shop { get; set; }
}