using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Blockip
    {
        public long Id { get; set; }
        public int Nodeid { get; set; }
        public string Ip { get; set; } = null!;
        public long Datetime { get; set; }
    }
}
