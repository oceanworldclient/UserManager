using AntDesign;

namespace UserManager.Razor.Utils;

public class Notify
{

    private readonly NotificationService _notification;
    
    public Notify(NotificationService notification)
    {
        _notification = notification;
    }
    
    public async void NoticeWithIcon(string title, string content, NotificationType type, double duration = 15)
    {
        await _notification.Open(new NotificationConfig()
        {
            Message = title,
            Description = content,
            NotificationType = type,
            Duration = duration
        });
    }
    
}