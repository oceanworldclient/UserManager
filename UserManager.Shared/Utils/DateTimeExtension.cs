namespace UserManager.Shared.Utils;

public static class DateTimeExtension
{
    public static string FormatString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}