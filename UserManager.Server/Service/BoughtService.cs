using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.EventHub;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Model;
using UserManager.Server.Utils;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Service;

public class BoughtService : BaseService<Bought, BoughtDto>
{
    private ShopService ShopService { get; }

    private ILogger<BoughtService> Log { get; }

    public BoughtService(IMapper mapper, ShopService shopService, ILogger<BoughtService> log) : base(mapper)
    {
        ShopService = shopService;
        Log = log;
    }

    public async Task<IList<BoughtDto>> GetByUserId(int userId, Website website)
    {
        try
        {
            var res = await GetByExpression(it => it.Userid == userId, website);
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
    }

    public async Task<BaseResult> BuyShop(BuyShopDto buyShopDto)
    {
        if (!await Verify(buyShopDto.UserEmail, buyShopDto.UserId, buyShopDto.Website))
        {
            Log.LogWarning("购买套餐入参 buyShopDto={@Dto}", buyShopDto);
            return new BaseResult() { Message = "参数不合法" };
        }
            
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
            if (user.Class > 0 && user.Class != shopContent!.Class)
                return new BaseResult() { Message = "用户当前套餐等级与购买套餐等级不一致" };

            var before = Mapper.Map<UserDto>(user);
            Log.LogInformation("购买套餐前：{@Before}", before);
            Log.LogInformation("套餐内容：{@Content}", shopContent);
            userDbSet.Attach(user);
            user.Money -= shop.Price;
            if (user.Class == 0)
            {
                before.ClassExpire = DateTime.Now.ToLocalTime();
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
            var after = Mapper.Map<UserDto>(user);
            Log.LogInformation("购买套餐后：{@After}", after);
            EventCenter.Instance.Publish(new BuyShopEvent()
            {
                Website = buyShopDto.Website,
                Payload = new BuyShopEventPayload()
                {
                    Shop = shop,
                    BeforeBought = before,
                    AfterBought = after
                }
            });
            return new BaseResult() { IsSuccess = true, Message = "购买成功" };
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]购买套餐异常");
            return new BaseResult() { Message = "购买失败" };
        }
    }

    private async Task<Bought?> FindLastBought(int userId)
    {
        var list = await DbContext!.Boughts
            .Where(it => it.Userid == userId)
            .OrderByDescending(it => it.Datetime)
            .Take(1)
            .ToListAsync();
        return list.Count == 0 ? null : list[0];
    }

    public async Task<BaseResult> Upgrade(BuyShopDto buyShopDto)
    {
        if (!await Verify(buyShopDto.UserEmail, buyShopDto.UserId, buyShopDto.Website))
            return new BaseResult() { Message = "参数不合法" };
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

            var delta = Math.Round((user.ClassExpire - DateTime.Now).TotalDays, 0);
            var equalMoney = ((decimal)delta / lastShoContent!.ClassExpire) * lastShop.Price;

            var tmpMoney = Math.Round(equalMoney + user.Money, 2);
            if (buyShopDto.IsQueryGap)
            {
                var diff = Math.Round(shop.Price - tmpMoney, 2);
                return new BaseResult() { IsSuccess = true, Message = diff > 0 ? $"需补{diff}元" : "无需补差价" };
            }

            if (tmpMoney < shop.Price - 0.2M)
                return new BaseResult() { Message = $"还差{Math.Round(shop.Price - tmpMoney, 2)}元" };


            var before = Mapper.Map<UserDto>(user);
            Log.LogInformation("购买套餐前：{@Before}", before);
            Log.LogInformation("套餐内容：{@Content}", shopContent);
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
            var after = Mapper.Map<UserDto>(user);
            Log.LogInformation("购买套餐后：{@After}", after);
            EventCenter.Instance.Publish(new UpgradeShopEvent()
            {
                Website = buyShopDto.Website,
                Payload = new UpgradeShopPayload()
                {
                    NewShop = shop,
                    OldShop = lastShop,
                    BeforeBought = before,
                    AfterBought = after
                }
            });
            return new BaseResult() { IsSuccess = true, Message = "购买成功" };
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]变更套餐异常");
            return new BaseResult() { Message = "购买失败" };
        }
    }

    private async Task<bool> Verify(string email, int userId, Website website)
    {
        try
        {
            InitialDbContext(website);
            var userBaseInfo = await DbContext!.Users
                .Where(it => it.Id == userId)
                .Select(it => new UserBaseInfoDto() { Id = it.Id, Email = it.Email, Website = website })
                .FirstAsync();
            if (email.Trim() == userBaseInfo.Email.Trim()) return true;
            Log.LogWarning("通过id查找到的用户{@User}, 入参：email={@Email} userid={@UserId}", userBaseInfo, email, userId);
            var op = AppHttpContext.Current.User;
            EventCenter.Instance.Publish(new IllegalOperationEvent()
            {
                Operator = op?.Identity?.Name ?? "",
                Website = website,
                Payload = new IllegalOperationPayload()
                {
                    UserBaseInfo = userBaseInfo,
                    Content = $"企图修改提交Id参数购买套餐: UserId={userId}, UserEmail={email}"
                }
            });
            return false;
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]验证异常");
            return false;
        }
    }

    private async Task<bool> Verify(long boughtId, int userId, Website website)
    {
        try
        {
            var dbSet = InitialDbContext(website);
            var userIdFromBought = await dbSet.Where(it => it.Id == boughtId).Select(it => it.Userid).FirstAsync();
            if (userIdFromBought == userId) return true;
            EventCenter.Instance.Publish(new IllegalOperationEvent()
            {
                Website = website,
                Payload = new IllegalOperationPayload()
                {
                    Content = "企图修改提交BoughtId参数修改其他用户购买信息",
                    UserBaseInfo = new UserBaseInfoDto() { Id = userId, Email = "UserId→" + userId }
                }
            });
            return false;
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]验证异常");
            return false;
        }
    }

    public async Task<BaseResult> DeleteById(DeleteBoughtDto dto)
    {
        try
        {
            if (!await Verify(dto.Id, dto.UserId, dto.Website)) return new BaseResult() { Message = "删除失败" };
            var dbSet = InitialDbContext(dto.Website);
            var bought = await dbSet.FindAsync(dto.Id);
            if (bought == null) return new BaseResult() { Message = "删除失败" };
            dbSet.Remove(bought);
            var res = await DbContext!.SaveChangesAsync() == 1;
            if (!res) return new BaseResult() { IsSuccess = false, Message = "删除失败" };
            EventCenter.Instance.Publish(new DeleteBoughtEvent()
            {
                Website = dto.Website,
                Payload = new DeleteBoughtPayload()
                {
                    Shop = (await ShopService.GetAllShops(dto.Website))[bought.Shopid],
                    UserBaseInfo = new UserBaseInfoDto()
                        { Id = dto.UserId, Email = dto.UserEmail, Website = dto.Website }
                }
            });
            return new BaseResult() { IsSuccess = true };
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]删除购买记录异常");
            return new BaseResult() { IsSuccess = false, Message = "删除失败" };
        }
    }

    public async Task<BaseResult> CloseRenew(CloseRenewDto dto)
    {
        try
        {
            if (!await Verify(dto.Id, dto.UserId, dto.Website)) return new BaseResult() { Message = "关闭失败" };
            var dbSet = InitialDbContext(dto.Website);
            var bought = await dbSet.FindAsync(dto.Id);
            if (bought == null) return new BaseResult() { Message = "删除失败" };
            bought.Renew = 0L;
            var res = await DbContext!.SaveChangesAsync() == 1;
            if (!res) return new BaseResult() { IsSuccess = false, Message = "操作失败" };
            EventCenter.Instance.Publish(new CloseRenewEvent()
            {
                Website = dto.Website,
                Payload = new CloseRenewPayload()
                {
                    Shop = (await ShopService.GetAllShops(dto.Website))[bought.Shopid],
                    UserBaseInfo = new UserBaseInfoDto()
                        { Id = dto.UserId, Email = dto.UserEmail, Website = dto.Website }
                }
            });
            return new BaseResult() { IsSuccess = true };
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]关闭续费异常");
            return new BaseResult() { IsSuccess = false, Message = "操作失败" };
        }
    }

    public async Task<BaseResult> RestoreBought(RestoreBoughtDto dto)
    {
        try
        {
            InitialDbContext(dto.Website);
            var users = await DbContext!.Users.Where(it => it.Id == dto.UserId && it.Email == dto.UserEmail).Select(
                it => new UserDto()
                {
                    Id = it.Id,
                    Class = it.Class,
                    ClassExpire = it.ClassExpire,
                    Email = dto.UserEmail
                }).Take(1).ToListAsync();
            if (users.Count == 0) return new BaseResult() { IsSuccess = false, Message = "用户不存在" };
            var user = users[0];
            if (user.Class != 0 || user.TransferEnable > 1000)
                return new BaseResult() { IsSuccess = false, Message = "请核对用户等级和流量信息" };
            if (user.ClassExpire <= DateTime.Now) return new BaseResult() { IsSuccess = false, Message = "用户等级已过期" };
            var lastBought = await FindLastBought(user.Id);
            if (lastBought == null) return new BaseResult() { IsSuccess = false, Message = "没有购买套餐" };
            if (lastBought.Datetime < DateTime.Now.Timestamp() - 3 * 4 * 3600)
                return new BaseResult() { IsSuccess = false, Message = "套餐购买时间早于3天" };
            var shop = await DbContext!.Set<Shop>().FindAsync(lastBought.Shopid);
            var shopContent = JsonSerializer.Deserialize<ShopContent>(shop!.Content);
            var saveUser = new User()
            {
                Id = user.Id,
                Class = shopContent!.Class,
                ClassExpire = DateTime.Now.AddDays(shopContent!.ClassExpire),
                TransferEnable = shopContent!.Bandwidth * 1024L * 1024 * 1024,
                Email = dto.UserEmail
            };
            DbContext!.Attach(saveUser);
            DbContext!.Entry(saveUser).Property(it => it.Class).IsModified = true;
            DbContext!.Entry(saveUser).Property(it => it.ClassExpire).IsModified = true;
            DbContext!.Entry(saveUser).Property(it => it.TransferEnable).IsModified = true;
            var res = (await DbContext!.SaveChangesAsync()) == 1;
            if (!res) return new BaseResult() { IsSuccess = false, Message = "后台保存异常" };
            EventCenter.Instance.Publish(new RestoreBoughtEvent()
            {
                UserEmail = dto.UserEmail,
                Website = dto.Website,
                Payload = new RestoreBoughtEventPayload()
                {
                    BeforeBought = user,
                    AfterBought = Mapper.Map<UserDto>(saveUser),
                    Shop = shop
                }
            });
            return new BaseResult() { IsSuccess = true };
        }
        catch(Exception e)
        {
            Serilog.Log.Fatal(e, "[BoughtService]恢复套餐异常");
            return new BaseResult() { IsSuccess = false, Message = "操作失败" };
        }
    }
}