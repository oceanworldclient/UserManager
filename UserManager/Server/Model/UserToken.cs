using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class UserToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public int CreateTime { get; set; }
        public int ExpireTime { get; set; }
    }
}
