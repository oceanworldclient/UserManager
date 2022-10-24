using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Ticket
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public long Rootid { get; set; }
        public long Userid { get; set; }
        public long Datetime { get; set; }
        public int Status { get; set; }
    }
}
