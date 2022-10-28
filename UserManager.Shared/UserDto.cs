using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml;
using UserManager.Shared.Converter;
using UserManager.Shared.Utils;

namespace UserManager.Shared;

public class UserDto
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime ClassExpire { get; set; }

    public int Class { get; set; }

    public int NodeGroup { get; set; }

    public int T { get; set; }

    public long U { get; set; }

    public long D { get; set; }

    public Website Website { get; set; }

    public decimal Money { get; set; }

    public string? ImValue { get; set; }

    public long TransferEnable { get; set; }

    public int RefBy { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime GroupExpire { get; set; }

    public double TotalInGb
    {
        get => Math.Round(TransferEnable / 1024.0 / 1024.0 / 1024.0, 2);
        set => TransferEnable = (long)value * 1024 * 1024 * 1024;
    }

    public string ClassExpireStr
    {
        get => ClassExpire.FormatString();
        set => ClassExpire = DateTimeExtension.FromString(value);
    }

    public string GroupExpireStr
    {
        get => GroupExpire.FormatString();
        set => GroupExpire = DateTimeExtension.FromString(value);
    }

    public double UsedInGb => Math.Round((U + D) / 1024.0 / 1024 / 1024, 2);

    public const string EMAIL = "邮箱";

    public const string USERNAME = "用户名";

    public const string CLASS = "等级";

    public const string CLASS_EXPIRE = "会员过期时间";

    public const string NODE_GROUP = "用户分组";

    public const string CONTACT = "用户联系方式";

    public const string MONEY = "余额";

    public const string TOTAL_IN_GB = "总流量";

    public const string TOTAL_USED = "已用流量";

    public const string REF_BY = "邀请人";

    public const string GROUP_EXPIRE = "分组到期时间";

    public string GetValue(string cnName)
    {
        return cnName switch
        {
            EMAIL => Email,
            USERNAME => UserName,
            CLASS => Class.ToString(),
            CLASS_EXPIRE => ClassExpire.FormatString(),
            CONTACT => ImValue ?? "",
            NODE_GROUP => NodeGroup.ToString(),
            MONEY => Money.ToString(),
            TOTAL_USED => UsedInGb.ToString(),
            TOTAL_IN_GB => TotalInGb.ToString(),
            REF_BY => RefBy.ToString(),
            GROUP_EXPIRE => GroupExpireStr,
            _ => ""
        };
    }

    public static IList<string> Keys = new List<string>()
    {
        EMAIL, USERNAME, CLASS, CLASS_EXPIRE, MONEY, CONTACT, NODE_GROUP, TOTAL_USED, TOTAL_IN_GB, REF_BY, GROUP_EXPIRE
    };
}