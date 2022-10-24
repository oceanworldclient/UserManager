using System;
using System.Collections.Generic;

namespace UserManager.Server.Model
{
    public partial class SsInviteCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
