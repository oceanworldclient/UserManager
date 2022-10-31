﻿using UserManager.Server.Constant;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;

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
        var log = new OperationLog()
        {
            UserEmail = payload.User!.Email,
            OptionTable = OperationLog.UserTable + "," + OperationLog.BoughtTable,
            Operator = e.Operator,
            Operation = OperationLogType.BuyShop,
            WebSite = e.Website.ToString(),
            Content = $"{payload.Shop!.Name}"
        };
        if (await OperationLogService.Save(log))
            TelegramBotService.PostMessage(TgBotMessage.FromOperationLog(log));
    }

    ~BuyShopLogger()
    {
        EventCenter.Instance.UnRegister(typeof(BuyShopEvent), this);
    }
}