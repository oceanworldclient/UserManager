namespace UserManager.Shared;

public class BuyShopDto : BaseDto
{
    public long ShopId { get; set; }
    
    public string UserEmail { get; set; }

    public int UserId { get; set; }
}