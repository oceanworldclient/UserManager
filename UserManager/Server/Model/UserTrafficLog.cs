using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class UserTrafficLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public long U { get; set; }
        public long D { get; set; }
        public int NodeId { get; set; }
        public float Rate { get; set; }
        public string Traffic { get; set; } = null!;
        public int LogTime { get; set; }
    }
}
