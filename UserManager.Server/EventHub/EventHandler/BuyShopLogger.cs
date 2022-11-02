using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.EventHub.EventHandler;

public class BuyShopLogger : AbsentEventHandler<BuyShopEvent>
{
    public BuyShopLogger()
    {
        EventCenter.Instance.Register(typeof(BuyShopEvent), this);
    }

    public override async void Handle(BuyShopEvent e)
    {
        var payload = e.Payload;
        var content = $"{payload.Shop!.Name}" + GetDiff(payload.BeforeBought, payload.AfterBought);
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
            .Append($"等级: {before.Class} -> {after.Class}").Append('\n')
            .Append($"余额: {before.Money} -> {after.Money}").Append('\n')
            .Append($"等级时间: {before.ClassExpireStr} -> {after.ClassExpireStr}")
            .Append($"分组: {before.NodeGroup} -> {after.NodeGroup}").Append('\n')
            .Append($"分组时间: {before.GroupExpireStr} -> {after.GroupExpireStr}").Append('\n')
            .Append($"流量: {before.TotalInGb}GB -> {after.TotalInGb}GB");
        return sb.ToString();
    }

    ~BuyShopLogger()
    {
        EventCenter.Instance.UnRegister(typeof(BuyShopEvent), this);
    }
}