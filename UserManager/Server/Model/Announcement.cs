using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class Announcement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; } = null!;
        public string Markdown { get; set; } = null!;
    }
}
