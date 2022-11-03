using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;
using UserManager.Shared;

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
        var content = $"{payload.OldShop!.Name} → {payload.NewShop!.Name}" +
                      GetDiff(payload.BeforeBought, payload.AfterBought);
        var log = new OperationLog()
        {
            UserEmail = payload.BeforeBought!.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.Upgrade,
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
            .Append($"余额: {before.Money} → {after.Money}, diff = {after.Money - before.Money}").Append('\n')
            .Append($"等级时间: {before.ClassExpireStr} → {after.ClassExpireStr}\n")
            .Append($"流量: {before.TotalInGb}GB → {after.TotalInGb}GB");
        if (before.GroupExpireStr != after.GroupExpireStr || before.NodeGroup != after.NodeGroup)
        {
            sb.Append('\n').Append($"分组: {before.NodeGroup} → {after.NodeGroup}").Append('\n')
                .Append($"分组时间: {before.GroupExpireStr} → {after.GroupExpireStr}");
        }

        return sb.ToString();
    }

    ~UpgradeShopLogger()
    {
        EventCenter.Instance.UnRegister(typeof(UpgradeShopEvent), this);
    }
}