using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Server.Model
{
    [Table("shop")]
    public class Shop
    {
        
        [Column("id")]
        public long Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; } = null!;
        
        [Column("price")]
        public decimal Price { get; set; }
        
        [Column("content")]
        public string Content { get; set; } = null!;
        
        [Column("auto_renew")]
        public int AutoRenew { get; set; }
        
        [Column("auto_reset_bandwidth")]
        public int AutoResetBandwidth { get; set; }
        
        [Column("status")]
        public int Status { get; set; }
    }
}
