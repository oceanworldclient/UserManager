using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.Entity;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;
using UserManager.Server.Utils;
using UserManager.Shared;

namespace UserManager.Server.EventHub.EventHandler;

public class RestoreBoughtLogger: AbsentEventHandler<RestoreBoughtEvent>
{
    public RestoreBoughtLogger()
    {
        EventCenter.Instance.Register(typeof(RestoreBoughtEvent), this);
    }

    public override async void Handle(RestoreBoughtEvent e)
    {
        var payload = e.Payload;
        var content = $"套餐: {payload.Shop!.Name}" + GetDiff(payload.BeforeBought, payload.AfterBought);
        var log = new OperationLog()
        {
            UserEmail = payload.BeforeBought!.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.BuyShop,
            WebSite = e.Website.ToString(),
            Content = content
        };
        if (await OperationLogService.Save(log))
            TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }

    private static string GetDiff(UserDto before, UserDto after)
    {
        var sb = new StringBuilder();
        sb.Append('\n')
            .Append($"等级: {before.Class} → {after.Class}").Append('\n')
            .Append($"等级时间: {before.ClassExpireStr} → {after.ClassExpireStr}, diff = {after.ClassExpire.CalDiffDays(before.ClassExpire)}天\n")
            .Append($"流量: {before.TotalInGb}GB → {after.TotalInGb}GB");
        return sb.ToString();
    }

    ~RestoreBoughtLogger()
    {
        EventCenter.Instance.UnRegister(typeof(BuyShopEvent), this);
    }
}