using UserManager.Server.Service;

namespace UserManager.Server;

public static class PanelServiceCollectionExtensions
{
    public static void AddPanelService(this IServiceCollection services)
    {
        services.AddScoped<BoughtService>();
        services.AddScoped<UserService>();
        services.AddScoped<ShopService>();
    }
}