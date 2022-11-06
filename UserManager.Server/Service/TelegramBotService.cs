using UserManager.Server.Model;

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
        try
        {
            await HttpClient.PostAsync(HttpClient.BaseAddress + msg.ToString(), null);
        }
        catch
        {

        }
    }

    public async void PostMessage(string msg)
    {
        try
        {
            await HttpClient.PostAsync(HttpClient.BaseAddress + msg, null);
        }
        catch
        {

        }      
    }
    
}