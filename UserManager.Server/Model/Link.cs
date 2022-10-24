using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Link
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Address { get; set; } = null!;
        public int Port { get; set; }
        public string Token { get; set; } = null!;
        public int Ios { get; set; }
        public long Userid { get; set; }
        public string? Isp { get; set; }
        public int? Geo { get; set; }
        public string? Method { get; set; }
    }
}
