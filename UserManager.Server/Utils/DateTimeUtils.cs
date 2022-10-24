namespace UserManager.Server.Utils;

public static class DateTimeUtils
{
    public static long Timestamp(this DateTime dt)
    {
        var dd = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        var utc = DateTime.SpecifyKind(dt, DateTimeKind.Utc);//本地时间转成UTC时间
        var ts = (utc - dd);
        return (long)ts.TotalSeconds;
    }

    public static DateTime FromTimestamp(this DateTime dt, long timestamp)
    {
        var dd = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        var ts = TimeSpan.FromSeconds(timestamp);
        return dd.Add(ts);
    }
    
}