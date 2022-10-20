using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Paylist
    {
        public long Id { get; set; }
        public long Userid { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public string? Tradeno { get; set; }
        public long Datetime { get; set; }
        public long? Shopid { get; set; }
        public long? Autorenew { get; set; }
    }
}
