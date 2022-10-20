using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    /// <summary>
    /// Telegram 任务列表
    /// </summary>
    public partial class TelegramTask
    {
        public uint Id { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Telegram Chat ID
        /// </summary>
        public string Chatid { get; set; } = null!;
        /// <summary>
        /// Telegram Message ID
        /// </summary>
        public string Messageid { get; set; } = null!;
        /// <summary>
        /// 任务详细内容
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// 临时任务进度
        /// </summary>
        public string? Process { get; set; }
        /// <summary>
        /// 网站用户 ID
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// Telegram User ID
        /// </summary>
        public string Tguserid { get; set; } = null!;
        /// <summary>
        /// 任务执行时间
        /// </summary>
        public long Executetime { get; set; }
        /// <summary>
        /// 任务产生时间
        /// </summary>
        public long Datetime { get; set; }
    }
}
