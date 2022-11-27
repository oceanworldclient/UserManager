using System.Globalization;
using UserManager.Shared.Enum;
using UserManager.Shared.Utils;

namespace UserManager.Shared;

public class BoughtDto
{
    public long Id { get; set; }

    public long Userid { get; set; }

    public long Shopid { get; set; }

    public string ShopName { get; set; }

    public long Datetime { get; set; }

    public long Renew { get; set; }

    public string Coupon { get; set; } = null!;

    public decimal Price { get; set; }

    public Website Website { get; set; }

    public string DatetimeString
    {
        get
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(Datetime);
            return dto.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo);
        }
        set => Datetime = DateTimeOffset.ParseExact(value, "yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo)
            .ToUnixTimeSeconds();
    }

    public string RenewStr
    {
        get
        {
            if (Renew == 0) return "不自动续费";
            var dto = DateTimeOffset.FromUnixTimeSeconds(Renew);
            return dto.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo);
        }
    }
    
}