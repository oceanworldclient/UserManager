using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    /// <summary>
    /// 用户订阅日志
    /// </summary>
    public partial class UserSubscribeLog
    {
        public uint Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 用户 ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// 获取的订阅类型
        /// </summary>
        public string? SubscribeType { get; set; }
        /// <summary>
        /// 请求 IP
        /// </summary>
        public string? RequestIp { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime? RequestTime { get; set; }
        /// <summary>
        /// 请求 UA 信息
        /// </summary>
        public string? RequestUserAgent { get; set; }
    }
}
