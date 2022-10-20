using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Shop
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Content { get; set; } = null!;
        public int AutoRenew { get; set; }
        public int AutoResetBandwidth { get; set; }
        public int Status { get; set; }
    }
}
