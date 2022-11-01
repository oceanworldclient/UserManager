using UserManager.Server.EventHub.EventHandler;
using UserManager.Server.Service;

namespace UserManager.Server;

public static class PanelServiceCollectionExtensions
{
    public static void AddPanelService(this IServiceCollection services)
    {
        services.AddScoped<BoughtService>();
        services.AddScoped<UserService>();
        services.AddScoped<ShopService>();
        services.AddScoped<OperationLogService>();
        services.AddSingleton(new BuyShopLogger());
        services.AddSingleton(new CloseRenewLogger());
        services.AddSingleton(new DeleteBoughtLogger());
        services.AddSingleton(new ModifyPasswordLogger());
        services.AddSingleton(new ModifyUserLogger());
        services.AddSingleton(new UpgradeShopLogger());
        services.AddSingleton(new IllegalOperationLogger());
    }
}