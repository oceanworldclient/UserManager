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
    private ShopService ShopService { get; }

    public BoughtService(IMapper mapper, ShopService shopService) : base(mapper)
    {
        ShopService = shopService;
    }

    public async Task<IList<BoughtDto>> GetByUserId(int userId, Website website)
    {
        try
        {
            var res = await GetByExpression(it => it.Userid == userId, website);
            if (!ShopService.AllShopDic.ContainsKey(website)) await ShopService.GetAllShops(website);
            foreach (var bought in res)
            {
                bought.ShopName = (await ShopService.GetShopById(bought.Shopid, website)).Name;
            }

            return res;
        }
        catch
        {
            return new List<BoughtDto>();
        }
        finally
        {
            Finish();
        }
    }

    public async Task<IList<BoughtDto>> GetLastTenByUserId(int userId, Website website)
    {
        try
        {
            var dbSet = InitialDbContext(website);
            var list = await dbSet.OrderByDescending(it => it.Datetime).Where(it => it.Userid == userId)
                .Take(10).ToListAsync();
            var res = Mapper.Map<List<BoughtDto>>(list);
            foreach (var bought in res)
            {
                bought.ShopName = (await ShopService.GetShopById(bought.Shopid, website)).Name;
            }

            return res;
        }
        catch
        {
            return new List<BoughtDto>();
        }
        finally
        {
            Finish();
        }
    }

    public async Task<BaseResult> BuyShop(BuyShopDto buyShopDto)
    {
        InitialDbContext(buyShopDto.Website);
        try
        {
            var userDbSet = DbContext!.Set<User>();
            var shop = await DbContext!.Set<Shop>().FindAsync(buyShopDto.ShopId);
            if (shop == null) return new BaseResult() { Message = "套餐不存在" };
            var user = await userDbSet.FindAsync(buyShopDto.UserId);
            if (user == null) return new BaseResult() { Message = "用户不存在" };
            if (user.Money < shop.Price) return new BaseResult() { Message = "余额不足" };
            var shopContent = JsonSerializer.Deserialize<ShopContent>(shop.Content);
            if (user.Class != 0 && user.Class != shopContent!.Class)
                return new BaseResult() { Message = "用户当前套餐等级与购买套餐等级不一致" };

            userDbSet.Attach(user);
            user.Money -= shop.Price;
            if (user.Class == 0)
            {
                user.ClassExpire = DateTime.Now.ToLocalTime().AddDays(shopContent!.ClassExpire);
                user.Class = shopContent!.Class;
                user.TransferEnable = shopContent!.Bandwidth * 1024 * 1024 * 1024L;
                user.U = 0;
                user.D = 0;
            }
            else
            {
                user.ClassExpire = user.ClassExpire.AddDays(shopContent!.ClassExpire);
                user.TransferEnable = shopContent!.Bandwidth * 1024 * 1024 * 1024L;
            }

            user.NodeConnector = shopContent!.Connector;
            // userDbSet.Update(user);
            var bought = new Bought()
            {
                Userid = user.Id,
                Shopid = shop.Id,
                Datetime = DateTime.Now.ToLocalTime().Timestamp(),
                Price = shop.Price,
                Coupon = "",
                Renew = DateTime.Now.ToLocalTime().AddDays(shopContent!.ClassExpire).Timestamp()
            };
            var lastBought = await FindLastBought(user.Id);
            if (lastBought != null)
            {
                DbContext!.Attach(lastBought);
                lastBought.Renew = 0L;
            }
            await DbContext.Set<Bought>().AddAsync(bought);
            await DbContext.SaveChangesAsync();
            return new BaseResult() { IsSuccess = true, Message = "购买成功" };
        }
        catch
        {
            return new BaseResult() { Message = "购买失败" };
        }
        finally
        {
            Finish();
        }
    }

    private async Task<Bought?> FindLastBought(int userId)
    {
        return await DbContext!.Boughts
            .Where(it => it.Userid == userId)
            .OrderByDescending(it => it.Datetime)
            .FirstAsync();
    }

    public async Task<BaseResult> Upgrade(BuyShopDto buyShopDto)
    {
        InitialDbContext(buyShopDto.Website);
        try
        {
            var userDbSet = DbContext!.Set<User>();
            var shop = await DbContext!.Set<Shop>().FindAsync(buyShopDto.ShopId);
            if (shop == null) return new BaseResult() { Message = "套餐不存在" };
            var user = await userDbSet.FindAsync(buyShopDto.UserId);
            if (user == null) return new BaseResult() { Message = "用户不存在" };
            var shopContent = JsonSerializer.Deserialize<ShopContent>(shop.Content);
            if (user.Class == 0 && user.Class == shopContent!.Class)
                return new BaseResult() { Message = "当前用户不需要升级套餐，请直接购买" };

            var lastBought = await FindLastBought(user.Id);
            if (lastBought == null) return new BaseResult() { Message = "当前用户不需要升级套餐，请直接购买" };
            var lastShop = await DbContext!.Shops.FindAsync(lastBought.Shopid);
            var lastShoContent = JsonSerializer.Deserialize<ShopContent>(lastShop!.Content);

            var delta = (user.ClassExpire - DateTime.Now).TotalDays;
            var equalMoney = ((decimal)delta / lastShoContent!.ClassExpire) * lastShop.Price;

            var tmpMoney = Math.Round(equalMoney + user.Money, 2);
            if (tmpMoney < shop.Price)
                return new BaseResult() { Message = $"还差{Math.Round(shop.Price - tmpMoney, 2)}元" };


            userDbSet.Attach(user);
            user.Money = tmpMoney - shop.Price;

            user.ClassExpire = DateTime.Now.AddDays(shopContent!.ClassExpire);
            user.Class = shopContent!.Class;
            user.TransferEnable = shopContent!.Bandwidth * 1024L * 1024 * 1024;
            user.U = 0;
            user.D = 0;

            user.NodeConnector = shopContent!.Connector;
            // userDbSet.Update(user);
            var bought = new Bought()
            {
                Userid = user.Id,
                Shopid = shop.Id,
                Datetime = DateTime.Now.ToLocalTime().Timestamp(),
                Price = shop.Price,
                Coupon = "",
                Renew = DateTime.Now.ToLocalTime().AddDays(shopContent!.ClassExpire).Timestamp()
            };
            DbContext!.Attach(lastBought);
            lastBought.Renew = 0L;
            await DbContext.Set<Bought>().AddAsync(bought);
            await DbContext.SaveChangesAsync();
            return new BaseResult() { IsSuccess = true, Message = "购买成功" };
        }
        catch
        {
            return new BaseResult() { Message = "购买失败" };
        }
        finally
        {
            Finish();
        }
    }

    public async Task<BaseResult> DeleteById(long id, Website website)
    {
        try
        {
            var dbSet = InitialDbContext(website);
            var bought = new Bought() { Id = id };
            dbSet.Attach(bought);
            dbSet.Remove(bought);
            var res = await DbContext!.SaveChangesAsync() == 1;
            if (res) return new BaseResult() { IsSuccess = true };
            return new BaseResult() { IsSuccess = false, Message = "删除失败" };
        }
        catch
        {
            return new BaseResult() { IsSuccess = false, Message = "删除失败" };
        }
        finally
        {
            Finish();
        }
    }

    public async Task<BaseResult> CloseRenew(long id, Website website)
    {
        try
        {
            var dbSet = InitialDbContext(website);
            var bought = new Bought() { Id = id, Renew = 0 };
            dbSet.Attach(bought);
            bought.Renew = 0L;
            DbContext!.Entry(bought).Property(it => it.Renew).IsModified = true;
            var res = await DbContext!.SaveChangesAsync() == 1;
            if (res) return new BaseResult() { IsSuccess = true };
            return new BaseResult() { IsSuccess = false, Message = "操作失败" };
        }
        catch
        {
            return new BaseResult() { IsSuccess = false, Message = "操作失败" };
        }
        finally
        {
            Finish();
        }
    }
}