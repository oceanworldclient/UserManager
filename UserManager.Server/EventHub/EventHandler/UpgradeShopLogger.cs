using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

namespace UserManager.Server.EventHub.EventHandler;

public class UpgradeShopLogger : AbsentEventHandler<UpgradeShopEvent>
{
    
    public UpgradeShopLogger()
    {
        EventCenter.Instance.Register(typeof(UpgradeShopEvent), this);
    }

    public override async void Handle(UpgradeShopEvent e)
    {
        var payload = e.Payload;
        var log = new OperationLog()
        {
            UserEmail = payload.User!.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.Upgrade,
            WebSite = e.Website.ToString(),
            Content = $"{payload.OldShop!.Name} -> {payload.NewShop!.Name}"
        };
        if (await OperationLogService.Save(log))
            TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }

    ~UpgradeShopLogger()
    {
        EventCenter.Instance.UnRegister(typeof(UpgradeShopEvent), this);
    }
}