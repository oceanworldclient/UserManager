using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SsNodeOnlineLog
    {
        public int Id { get; set; }
        public int NodeId { get; set; }
        public int OnlineUser { get; set; }
        public int LogTime { get; set; }
    }
}
