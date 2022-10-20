using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Code
    {
        public long Id { get; set; }
        public string Code1 { get; set; } = null!;
        public int Type { get; set; }
        public decimal Number { get; set; }
        public int Isused { get; set; }
        public long Userid { get; set; }
        public DateTime Usedatetime { get; set; }
        public string? Tradeno { get; set; }
    }
}
