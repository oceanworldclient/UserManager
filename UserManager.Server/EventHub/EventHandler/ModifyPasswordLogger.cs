using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

namespace UserManager.Server.EventHub.EventHandler;

public class ModifyPasswordLogger:AbsentEventHandler<ModifyPasswordEvent>
{

    private ModifyPasswordLogger _logger = new();

    private ModifyPasswordLogger()
    {
        EventHub.Instance.Register(typeof(ModifyPasswordEvent), this);
    }
    
    public override async void Handle(ModifyPasswordEvent e)
    {
        var payload = e.Payload;
        var log = new OperationLog()
        {
            UserEmail = payload.UserMail,
            OptionTable = OperationLog.UserTable,
            Operator = e.Operator,
            Operation = OperationLogType.ModifyPassword,
            WebSite = payload.Website.ToString(),
        };
        if(await OperationLogService.Save(log)) TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }
    
}