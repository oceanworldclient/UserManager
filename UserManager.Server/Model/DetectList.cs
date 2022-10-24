using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class DetectList
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Regex { get; set; } = null!;
        public int Type { get; set; }
    }
}
