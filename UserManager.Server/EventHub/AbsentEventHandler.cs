using UserManager.Server.Service;
using UserManager.Server.Utils;

namespace UserManager.Server.EventHub;

public abstract class AbsentEventHandler<T>  
{

    public abstract void Handle(T e);

    protected OperationLogService OperationLogService => AppHttpContext.GetSerivce<OperationLogService>();
    
    protected TelegramBotService TelegramBotService => AppHttpContext.GetSerivce<TelegramBotService>();

}