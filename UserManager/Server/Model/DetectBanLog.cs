using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    /// <summary>
    /// 审计封禁日志
    /// </summary>
    public partial class DetectBanLog
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
        /// 本次违规次数
        /// </summary>
        public int DetectNumber { get; set; }
        /// <summary>
        /// 本次封禁时长
        /// </summary>
        public int BanTime { get; set; }
        /// <summary>
        /// 统计开始时间
        /// </summary>
        public long StartTime { get; set; }
        /// <summary>
        /// 统计结束时间
        /// </summary>
        public long EndTime { get; set; }
        /// <summary>
        /// 累计违规次数
        /// </summary>
        public int AllDetectNumber { get; set; }
    }
}
