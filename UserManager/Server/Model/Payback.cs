using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Payback
    {
        public long Id { get; set; }
        public decimal Total { get; set; }
        public long Userid { get; set; }
        public long RefBy { get; set; }
        public decimal RefGet { get; set; }
        public long Datetime { get; set; }
    }
}
