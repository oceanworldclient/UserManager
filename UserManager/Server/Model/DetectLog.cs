using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class DetectLog
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ListId { get; set; }
        public long Datetime { get; set; }
        public int NodeId { get; set; }
    }
}
