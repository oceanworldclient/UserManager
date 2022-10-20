using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SmsVerify
    {
        public ulong Id { get; set; }
        public long Phone { get; set; }
        public string Code { get; set; } = null!;
        public string Ip { get; set; } = null!;
        public long ExpireIn { get; set; }
    }
}
