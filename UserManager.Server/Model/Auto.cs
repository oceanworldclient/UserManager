using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Auto
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public string Sign { get; set; } = null!;
        public long Datetime { get; set; }
    }
}
