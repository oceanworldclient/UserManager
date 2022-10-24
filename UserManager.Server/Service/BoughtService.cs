using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.Model;
using UserManager.Server.Utils;
using UserManager.Shared;
using UserManager.Shared.Response;

namespace UserManager.Server.Service;

public class BoughtService : BaseService<Bought, BoughtDto>
{
    public BoughtService(IMapper mapper, ConfigurationManager configuration) : base(mapper, configuration)
    {
    }

    public async Task<IList<BoughtDto>> GetByUserId(int userId, Website website)
    {
        return await GetByExpression(it => it.Userid == userId, website);
    }

    public async Task<IList<BoughtDto>> GetLastTenByUserId(int userId, Website website)
    {
        InitialDbContext(website);
        var list = await DbContext!.Set<Bought>().OrderByDescending(it => it.Datetime).Where(it => it.Userid == userId)
            .Take(10).ToListAsync();
        return Mapper.Map<List<BoughtDto>>(list);
    }

    public async Task<BaseResult> BuyShop(BoughtShopDto boughtShopDto)
    {
        InitialDbContext(boughtShopDto.Website);
        var userDbSet = DbContext!.Set<User>(); 
        var shop = await DbContext!.Set<Shop>().FindAsync(boughtShopDto.ShopId);
        if (shop == null) return new BaseResult(){Message = "套餐不存在"};
        var user = await userDbSet.FindAsync(boughtShopDto.UserId);
        if (user == null) return new BaseResult() { Message = "用户不存在" };
        if (user.Money < shop.Price) return new BaseResult() { Message = "余额不足" };
        var shopContent = JsonSerializer.Deserialize<ShopContent>(shop.Content);
        if (user.Class != 0 && user.Class != shopContent!.Class)
            return new BaseResult() { Message = "用户当前套餐等级与购买套餐等级不一致" };
        try
        {
            userDbSet.Attach(user);
            user.Money -= shop.Price;
            if (user.Class == 0)
            {
                user.ClassExpire = DateTime.Now.AddDays(shopContent!.ClassExpire);
                user.Class = shopContent!.Class;
                user.TransferEnable = shopContent!.Bandwidth * 1024 * 1024 * 1024;
                user.U = 0;
                user.D = 0;
            }
            else
            {
                user.ClassExpire = user.ClassExpire.AddDays(shopContent!.ClassExpire);
                user.TransferEnable += shopContent!.Bandwidth * 1024 * 1024 * 1024;
            }

            user.NodeConnector = shopContent!.Connector;
            userDbSet.Update(user);
            var bought = new Bought()
            {
                Userid = user.Id,
                Shopid = shop.Id,
                Datetime = DateTime.Now.Timestamp(),
                Coupon = "",
                Renew = DateTime.Now.AddDays(shopContent!.ClassExpire).Timestamp()
            };
            await DbContext.Set<Bought>().AddAsync(bought);
            await DbContext.SaveChangesAsync();
            return new BaseResult() { IsSuccess = true, Message = "购买成功" };
        }
        catch
        {
            return new BaseResult() { Message = "购买失败" };
        }

    }
    

}