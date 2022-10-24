using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Unblockip
    {
        public long Id { get; set; }
        public string Ip { get; set; } = null!;
        public long Datetime { get; set; }
        public long Userid { get; set; }
    }
}
