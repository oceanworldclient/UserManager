using UserManager.Server.Model;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace UserManager.Server.Service;

public class TelegramBotService
{
    private HttpClient HttpClient { get; }
    
    public TelegramBotService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async void PostMessage(TgBotMessage msg)
    {
        await HttpClient.PostAsync(HttpClient.BaseAddress + msg.ToString(), null);
    }

    public async void PostMessage(string msg)
    {
        await HttpClient.PostAsync(HttpClient.BaseAddress + msg, null);
    }
    
}