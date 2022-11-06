using UserManager.Server.Constant;
using UserManager.Server.Entity;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

namespace UserManager.Server.EventHub.EventHandler;

public class DeleteBoughtLogger : AbsentEventHandler<DeleteBoughtEvent>
{
    public DeleteBoughtLogger()
    {
        EventCenter.Instance.Register(typeof(DeleteBoughtEvent), this);
    }

    public override async void Handle(DeleteBoughtEvent e)
    {
        var payload = e.Payload;
        var log = new OperationLog()
        {
            UserEmail = payload.UserBaseInfo!.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.DeleteBought,
            WebSite = e.Website.ToString(),
            Content = $"{payload.Shop!.Name}"
        };
        if (await OperationLogService.Save(log))
            TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }

    ~DeleteBoughtLogger()
    {
        EventCenter.Instance.UnRegister(typeof(DeleteBoughtEvent), this);
    }
}