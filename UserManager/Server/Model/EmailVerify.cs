using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class EmailVerify
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string Ip { get; set; } = null!;
        public string Code { get; set; } = null!;
        public long ExpireIn { get; set; }
    }
}
