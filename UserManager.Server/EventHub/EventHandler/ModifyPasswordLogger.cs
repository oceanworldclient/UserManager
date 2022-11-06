using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.Entity;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

namespace UserManager.Server.EventHub.EventHandler;

public class ModifyPasswordLogger : AbsentEventHandler<ModifyPasswordEvent>
{
    public ModifyPasswordLogger()
    {
        EventCenter.Instance.Register(typeof(ModifyPasswordEvent), this);
    }

    public override async void Handle(ModifyPasswordEvent e)
    {
        var payload = e.Payload;
        var log = new OperationLog()
        {
            UserEmail = payload.UserBaseInfo.Email,
            OptionTable = OperationLog.UserTable,
            Operator = e.Operator,
            Operation = OperationLogType.ModifyPassword,
            WebSite = payload.UserBaseInfo.Website.ToString(),
        };
        if (await OperationLogService.Save(log)) TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }

    ~ModifyPasswordLogger()
    {
        EventCenter.Instance.UnRegister(typeof(ModifyPasswordEvent), this);
    }
}