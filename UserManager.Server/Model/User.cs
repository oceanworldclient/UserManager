using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long? Phone { get; set; }
        public string Pass { get; set; } = null!;
        public string Passwd { get; set; } = null!;
        public int T { get; set; }
        public long U { get; set; }
        public long D { get; set; }
        public string Plan { get; set; } = null!;
        public long TransferEnable { get; set; }
        public int Port { get; set; }
        public sbyte Switch { get; set; }
        public sbyte Enable { get; set; }
        public int DetectBan { get; set; }
        public DateTime? LastDetectBanTime { get; set; }
        public int AllDetectNumber { get; set; }
        public sbyte Type { get; set; }
        public int LastGetGiftTime { get; set; }
        public int LastCheckInTime { get; set; }
        public int LastRestPassTime { get; set; }
        public DateTime RegDate { get; set; }
        public int InviteNum { get; set; }
        public decimal Money { get; set; }
        public int RefBy { get; set; }
        public int ExpireTime { get; set; }
        public string Method { get; set; } = null!;
        public sbyte IsEmailVerify { get; set; }
        public string RegIp { get; set; } = null!;
        public decimal NodeSpeedlimit { get; set; }
        public int NodeConnector { get; set; }
        public int IsAdmin { get; set; }
        public int? ImType { get; set; }
        public string? ImValue { get; set; }
        public long LastDayT { get; set; }
        public int SendDailyMail { get; set; }
        public int Class { get; set; }
        public DateTime ClassExpire { get; set; }
        public DateTime ExpireIn { get; set; }
        public string Theme { get; set; } = null!;
        public string GaToken { get; set; } = null!;
        public int GaEnable { get; set; }
        public string? Pac { get; set; }
        public string? Remark { get; set; }
        public int NodeGroup { get; set; }
        public DateTime? GroupExpire { get; set; }
        public int AutoResetDay { get; set; }
        public decimal AutoResetBandwidth { get; set; }
        public string? Protocol { get; set; }
        public string? ProtocolParam { get; set; }
        public string? Obfs { get; set; }
        public string? ObfsParam { get; set; }
        public string? ForbiddenIp { get; set; }
        public string? ForbiddenPort { get; set; }
        public string? DisconnectIp { get; set; }
        public int IsHide { get; set; }
        public int IsMultiUser { get; set; }
        public long? TelegramId { get; set; }
        public long? Discord { get; set; }
        public string? Uuid { get; set; }
        /// <summary>
        /// 用户的语言
        /// </summary>
        public string Lang { get; set; } = null!;
    }
}
