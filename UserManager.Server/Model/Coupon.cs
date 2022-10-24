using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Coupon
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public int Onetime { get; set; }
        public long Expire { get; set; }
        public string Shop { get; set; } = null!;
        public int Credit { get; set; }
    }
}
