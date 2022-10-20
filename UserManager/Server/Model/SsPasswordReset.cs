using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SsPasswordReset
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int InitTime { get; set; }
        public int ExpireTime { get; set; }
    }
}
