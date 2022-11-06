using System.Globalization;
using System.Text;
using UserManager.Server.Constant;
using UserManager.Server.Entity;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;
using UserManager.Server.Utils;

namespace UserManager.Server.EventHub.EventHandler;

public class ModifyUserLogger : AbsentEventHandler<ModifyUserEvent>
{
    public ModifyUserLogger()
    {
        EventCenter.Instance.Register(typeof(ModifyUserEvent), this);
    }

    public override async void Handle(ModifyUserEvent e)
    {
        var oldDto = e.Payload[0];
        var newDto = e.Payload[1];

        List<OperationLog> operationLogs = new();
        var baseLog = new OperationLog()
        {
            UserEmail = oldDto.Email,
            OptionTable = OperationLog.UserTable,
            Operator = e.Operator,
            WebSite = newDto.Website.ToString()
        };
        StringBuilder sb = new();
        sb.Append($"网站: {baseLog.WebSite}\n")
            .Append($"操作人: {baseLog.Operator}\n")
            .Append($"操作: 更新用户信息\n")
            .Append($"用户: {baseLog.UserEmail}\n");

        if (Math.Abs(oldDto.ClassExpire.Timestamp() - newDto.ClassExpire.Timestamp()) > 3600 * 24)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.ClassExpireStr,
                newDto.ClassExpireStr, OperationLogType.ModifyClassExpire, sb);
            operationLogs.Add(log);
            sb.Append($", diff = {newDto.ClassExpire.CalDiffDays(oldDto.ClassExpire)}天");
        }

        if (oldDto.Class != newDto.Class)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.Class.ToString(),
                newDto.Class.ToString(),
                OperationLogType.ModifyClass, sb);
            operationLogs.Add(log);
        }

        if (oldDto.NodeGroup != newDto.NodeGroup)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.NodeGroup.ToString(),
                newDto.NodeGroup.ToString(),
                OperationLogType.ModifyNodeGroup, sb);
            operationLogs.Add(log);
        }

        if (oldDto.Money != newDto.Money)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.Money.ToString(CultureInfo.CreateSpecificCulture("zh-cn")),
                newDto.Money.ToString(CultureInfo.CreateSpecificCulture("zh-cn")),
                OperationLogType.ModifyMoney, sb);
            sb.Append($", diff = {Math.Round(newDto.Money - oldDto.Money)}");
            operationLogs.Add(log);
        }

        if (oldDto.ImValue != newDto.ImValue)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.ImValue ?? "",
                newDto.ImValue ?? "",
                OperationLogType.ModifyImValue, sb);
            operationLogs.Add(log);
        }

        if (oldDto.TransferEnable != newDto.TransferEnable)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.TotalInGb + "GB",
                newDto.TotalInGb + "GB",
                OperationLogType.ModifyTransferEnable, sb);
            operationLogs.Add(log);
        }

        if (oldDto.RefBy != newDto.RefBy)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.RefBy.ToString(),
                newDto.RefBy.ToString(),
                OperationLogType.ModifyRefBy, sb);
            operationLogs.Add(log);
        }

        if (oldDto.UserName != newDto.UserName)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.UserName,
                newDto.UserName,
                OperationLogType.ModifyUserName, sb);
            operationLogs.Add(log);
        }

        if (Math.Abs(oldDto.GroupExpire.Timestamp() - newDto.GroupExpire.Timestamp()) > 3600 * 24)
        {
            var log = CreateNewLogFromPattern(baseLog,
                oldDto.GroupExpireStr,
                newDto.GroupExpireStr, 
                OperationLogType.ModifyNodeGroupExpire, sb);
            operationLogs.Add(log);
        }

        if (await OperationLogService.Save(operationLogs.ToArray()))
            TelegramBotService.PostMessage(sb.ToString());
    }

    private static OperationLog CreateNewLogFromPattern(OperationLog baseLog, 
        string oldValue,
        string newValue,
        string operationType,
        StringBuilder sb)
    {
        var log = baseLog.Copy();
        log.Operation = operationType;
        log.OldValue = oldValue;
        log.NewValue = newValue;
        log.Content = $"{log.OldValue} → {log.NewValue}";
        sb.Append($"\n{operationType}: {log.Content}");
        return log;
    }
    
    ~ModifyUserLogger()
    {
        EventCenter.Instance.UnRegister(typeof(ModifyUserEvent), this);
    }

}