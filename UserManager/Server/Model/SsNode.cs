using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SsNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public string Server { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int Sort { get; set; }
        public bool CustomMethod { get; set; }
        public float TrafficRate { get; set; }
        public int NodeClass { get; set; }
        public decimal NodeSpeedlimit { get; set; }
        public int NodeConnector { get; set; }
        public long NodeBandwidth { get; set; }
        public long NodeBandwidthLimit { get; set; }
        public int BandwidthlimitResetday { get; set; }
        public long NodeHeartbeat { get; set; }
        public string? NodeIp { get; set; }
        public int NodeGroup { get; set; }
        public int CustomRss { get; set; }
        public int? MuOnly { get; set; }
    }
}
