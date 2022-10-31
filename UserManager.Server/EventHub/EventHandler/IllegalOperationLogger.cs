using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

namespace UserManager.Server.EventHub.EventHandler;

public class IllegalOperationLogger:AbsentEventHandler<IllegalOperationEvent>
{
    public IllegalOperationLogger()
    {
        EventCenter.Instance.Register(typeof(IllegalOperationEvent), this);
    }
    
    public override async void Handle(IllegalOperationEvent e)
    {
        var payload = e.Payload;
        var log = new OperationLog()
        {
            UserEmail = payload.UserBaseInfo.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.Emergency,
            WebSite = payload.UserBaseInfo.Website.ToString(),
            Content = payload.Content
        };
        if (await OperationLogService.Save(log))
            TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }
    
    ~IllegalOperationLogger()
    {
        EventCenter.Instance.UnRegister(typeof(IllegalOperationEvent), this);
    }
}