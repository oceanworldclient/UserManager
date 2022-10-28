using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Server.Model
{
    [Table("bought")]
    public class Bought
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        
        [Column("userid")]
        public long Userid { get; set; }
        
        [Column("shopid")]
        public long Shopid { get; set; }
        
        [Column("datetime")]
        public long Datetime { get; set; }
        
        [Column("renew")]
        public long Renew { get; set; }
        
        [Column("coupon")]
        public string Coupon { get; set; } = null!;
        
        [Column("price")]
        public decimal Price { get; set; }
        
    }
}
