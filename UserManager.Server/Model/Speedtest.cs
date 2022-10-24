using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Speedtest
    {
        public long Id { get; set; }
        public int Nodeid { get; set; }
        public long Datetime { get; set; }
        public string Telecomping { get; set; } = null!;
        public string Telecomeupload { get; set; } = null!;
        public string Telecomedownload { get; set; } = null!;
        public string Unicomping { get; set; } = null!;
        public string Unicomupload { get; set; } = null!;
        public string Unicomdownload { get; set; } = null!;
        public string Cmccping { get; set; } = null!;
        public string Cmccupload { get; set; } = null!;
        public string Cmccdownload { get; set; } = null!;
    }
}
