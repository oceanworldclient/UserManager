using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SsNodeInfo
    {
        public int Id { get; set; }
        public int NodeId { get; set; }
        public float Uptime { get; set; }
        public string Load { get; set; } = null!;
        public int LogTime { get; set; }
    }
}
