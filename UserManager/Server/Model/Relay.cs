using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Relay
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long SourceNodeId { get; set; }
        public long DistNodeId { get; set; }
        public string DistIp { get; set; } = null!;
        public int Port { get; set; }
        public int Priority { get; set; }
    }
}
