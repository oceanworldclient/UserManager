namespace UserManager.Shared;

public class BoughtDto
{
    public long Id { get; set; }
    
    public long Userid { get; set; }
    
    public long Shopid { get; set; }
    
    public long Datetime { get; set; }
    
    public long Renew { get; set; }
    
    public string Coupon { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public Website Website { get; set; }
    
}