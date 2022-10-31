using System.Globalization;

namespace UserManager.Shared.Utils;

public static class DateTimeExtension
{
    public static string FormatString(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("zh-cn"));
    }

    public static DateTime FromString(string s)
    {
        return DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("zh-cn"));
    } 

}

