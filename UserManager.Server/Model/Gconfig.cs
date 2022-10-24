using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    /// <summary>
    /// 网站配置
    /// </summary>
    public partial class Gconfig
    {
        /// <summary>
        /// 配置名
        /// </summary>
        public string Key { get; set; } = null!;
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; } = null!;
        /// <summary>
        /// 之前的配置值
        /// </summary>
        public string Oldvalue { get; set; } = null!;
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 配置描述
        /// </summary>
        public string Comment { get; set; } = null!;
        /// <summary>
        /// 操作员 ID
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName { get; set; } = null!;
        /// <summary>
        /// 操作员邮箱
        /// </summary>
        public string OperatorEmail { get; set; } = null!;
        /// <summary>
        /// 修改时间
        /// </summary>
        public long LastUpdate { get; set; }
    }
}
