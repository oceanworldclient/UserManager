namespace UserManager.Server.Utils;

public static class DateTimeUtils
{
    public static long Timestamp(this DateTime dt)
    {
        return new DateTimeOffset(dt).ToUnixTimeSeconds();
    }

    public static DateTime FromTimestamp(this DateTime dt, long timestamp)
    {
        var dd = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        var ts = TimeSpan.FromSeconds(timestamp);
        return dd.Add(ts);
    }

    public static double CalDiffDays(this DateTime dt, DateTime other)
    {
        var diff = dt - other;
        return Math.Round(diff.TotalDays, 1);
    }

}