using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Server.Model
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("user_name")]
        public string UserName { get; set; } = null!;
        
        [Column("email")]
        public string Email { get; set; } = null!;
        
        [Column("phone")]
        public long? Phone { get; set; }
        
        [Column("pass")]
        public string Pass { get; set; } = null!;
        
        [Column("passwd")]
        public string Passwd { get; set; } = null!;

        [Column("t")]
        public int T { get; set; }
        
        [Column("u")]
        public long U { get; set; }
        
        [Column("d")]
        public long D { get; set; }
        
        [Column("plan")]
        public string Plan { get; set; } = null!;
        
        [Column("transfer_enable")]
        public long TransferEnable { get; set; }
        
        [Column("port")]
        public int Port { get; set; }
        
        [Column("switch")]
        public sbyte Switch { get; set; }
        
        [Column("enable")]
        public sbyte Enable { get; set; }
        
        [Column("detect_ban")]
        public int DetectBan { get; set; }
        
        [Column("last_detect_ban_time")]
        public DateTime? LastDetectBanTime { get; set; }
        
        [Column("all_detect_number")]
        public int AllDetectNumber { get; set; }
        
        [Column("type")]
        public sbyte Type { get; set; }
        
        [Column("last_get_gift_time")]
        public int LastGetGiftTime { get; set; }
        
        [Column("last_check_in_time")]
        public int LastCheckInTime { get; set; }
        
        [Column("last_rest_pass_time")]
        public int LastRestPassTime { get; set; }
        
        [Column("reg_date")]
        public DateTime RegDate { get; set; }
        
        [Column("invite_num")]
        public int InviteNum { get; set; }
        
        [Column("money")]
        public decimal Money { get; set; }
        
        [Column("ref_by")]
        public int RefBy { get; set; }
        
        [Column("expire_time")]
        public int ExpireTime { get; set; }
        
        [Column("method")]
        public string Method { get; set; } = null!;
        
        [Column("is_email_verify")]
        public sbyte IsEmailVerify { get; set; }
        
        [Column("reg_ip")]
        public string RegIp { get; set; } = null!;
        
        [Column("node_speedlimit")]
        public decimal NodeSpeedlimit { get; set; }
        
        [Column("node_connector")]
        public int NodeConnector { get; set; }
        
        [Column("is_admin")]
        public int IsAdmin { get; set; }
        
        [Column("im_type")]
        public int? ImType { get; set; }
        
        [Column("im_value")]
        public string? ImValue { get; set; }
        
        [Column("last_day_t")]
        public long LastDayT { get; set; }
        
        [Column("sendDailyMail")]
        public int SendDailyMail { get; set; }
        
        [Column("class")]
        public int Class { get; set; }
        
        [Column("class_expire")]
        public DateTime ClassExpire { get; set; }
        
        [Column("expire_in")]
        public DateTime ExpireIn { get; set; }
        
        [Column("theme")]
        public string Theme { get; set; } = null!;
        
        [Column("ga_token")]
        public string GaToken { get; set; } = null!;
        
        [Column("ga_enable")]
        public int GaEnable { get; set; }
        
        [Column("pac")]
        public string? Pac { get; set; }
        
        [Column("remark")]
        public string? Remark { get; set; }
        
        [Column("node_group")]
        public int NodeGroup { get; set; }
        
        [Column("group_expire")]
        public DateTime GroupExpire { get; set; }
        
        [Column("auto_reset_day")]
        public int AutoResetDay { get; set; }
        
        [Column("auto_reset_bandwidth")]
        public decimal AutoResetBandwidth { get; set; }
        
        [Column("protocol")]
        public string? Protocol { get; set; }
        
        [Column("protocol_param")]
        public string? ProtocolParam { get; set; }
        
        [Column("obfs")]
        public string? Obfs { get; set; }
        
        [Column("obfs_param")]
        public string? ObfsParam { get; set; }
        
        [Column("forbidden_ip")]
        public string? ForbiddenIp { get; set; }
        
        [Column("forbidden_port")]
        public string? ForbiddenPort { get; set; }
        
        [Column("disconnect_ip")]
        public string? DisconnectIp { get; set; }
        
        [Column("is_hide")]
        public int IsHide { get; set; }
        
        [Column("is_multi_user")]
        public int IsMultiUser { get; set; }
        
        [Column("telegram_id")]
        public long? TelegramId { get; set; }
        
        [Column("discord")]
        public long? Discord { get; set; }
        
        [Column("uuid")]
        public string? Uuid { get; set; }
        
        /// <summary>
        /// 用户的语言
        /// </summary>
        [Column("lang")]
        public string Lang { get; set; } = null!;
    }
}
