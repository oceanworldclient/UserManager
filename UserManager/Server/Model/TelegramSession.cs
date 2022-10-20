using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class TelegramSession
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Type { get; set; }
        public string SessionContent { get; set; } = null!;
        public long Datetime { get; set; }
    }
}
