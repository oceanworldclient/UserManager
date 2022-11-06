using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.EventHub;
using UserManager.Server.EventHub.Event;
using UserManager.Server.Utils;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Service;

public class UserService : BaseService<User, UserDto>
{
    public UserService(IMapper mapper) : base(mapper)
    {
    }

    public new async Task<IList<UserDto>> GetById(int id, Website website)
    {
        try
        {
            return await GetByExpression(it => it.Id == id && it.IsAdmin == 0, GetSelector(website), website);
        }
        catch
        {
            return new List<UserDto>();
        }
    }

    public async Task<IList<UserDto>> GetByEmail(string email, Website website)
    {
        try
        {
            return await TakeByExpression(it => it.Email == email && it.IsAdmin == 0, GetSelector(website), website);
        }
        catch
        {
            return new List<UserDto>();
        }
    }

    public async Task<IList<UserDto>> GetByContact(string contact, Website website)
    {
        try
        {
            return await TakeByExpression(it => it.ImValue != null && it.ImValue.Contains(contact) && it.IsAdmin == 0,
                GetSelector(website),
                website);
        }
        catch
        {
            return new List<UserDto>();
        }
    }

    public async Task<IList<UserDto>> GetByUserName(string username, Website website)
    {
        if (username == "") return new List<UserDto>();
        try
        {
            return await TakeByExpression(it => it.UserName.Contains(username) && it.IsAdmin == 0, GetSelector(website),
                website);
        }
        catch
        {
            return new List<UserDto>();
        }
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        try
        {
            if (!await VerifyUser(dto.UserEmail, dto.UserId, dto.Website)) return false;
            var dbSet = InitialDbContext(dto.Website);
            HandlerPassword(dto);
            var user = new User()
            {
                Id = dto.UserId,
                Pass = dto.NewPassword
            };
            dbSet.Attach(user);
            DbContext!.Entry(user).Property(u => u.Pass).IsModified = true;
            if (await DbContext!.SaveChangesAsync() != 1) return false;
            EventCenter.Instance.Publish(new ModifyPasswordEvent()
            {
                Website = dto.Website,
                Payload = new ModifyPasswordPayload()
                {
                    UserBaseInfo = new UserBaseInfoDto()
                        { Id = dto.UserId, Email = dto.UserEmail, Website = dto.Website }
                }
            });
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<BaseResult> ModifyRefBy(ModifyRefByDto dto)
    {
        try
        {
            if (!await VerifyUser(dto.UserEmail, dto.UserId, dto.Website))
                return new BaseResult() { Message = "异常，修改失败" };
            if (dto.UserEmail == dto.RefBy) return new BaseResult() { Message = "不能修改成自己" };
            var dbSet = InitialDbContext(dto.Website);
            var refBy = await dbSet
                .Where(it => it.Email == dto.RefBy)
                .Select(it => new UserBaseInfoDto()
                {
                    Id = it.Id,
                    Email = it.Email,
                    Website = dto.Website
                }).Take(1).ToListAsync();
            if (refBy.Count == 0) return new BaseResult() { Message = $"{dto.RefBy} 不存在" };
            var user = new User()
            {
                Id = dto.UserId,
                RefBy = refBy[0].Id
            };
            dbSet.Attach(user);
            DbContext!.Entry(user).Property(u => u.Pass).IsModified = true;
            if (await DbContext!.SaveChangesAsync() != 1) return new BaseResult() { Message = "异常，修改失败" };
            EventCenter.Instance.Publish(new ModifyPasswordEvent()
            {
                Website = dto.Website,
                Payload = new ModifyPasswordPayload()
                {
                    UserBaseInfo = new UserBaseInfoDto()
                        { Id = dto.UserId, Email = dto.UserEmail, Website = dto.Website }
                }
            });
            return new BaseResult() { IsSuccess = true, Message = "" };
        }
        catch
        {
            return new BaseResult() { Message = "异常，修改失败" };
        }
    }


    private static void HandlerPassword(ModifyPasswordDto dto)
    {
        switch (dto.Website)
        {
            case Website.World:
                dto.NewPassword = SHA256Utils.Encrypt(dto.NewPassword, "haha");
                return;
            case Website.Ocean:
                dto.NewPassword = SHA256Utils.Encrypt(dto.NewPassword);
                return;
            case Website.Zebra:
                dto.NewPassword = MD5Utils.Encrypt(dto.NewPassword);
                return;
            default:
                return;
        }
    }

    public async Task<bool> ModifyUser(UserDto userDto)
    {
        try
        {
            if (!await VerifyUser(userDto.Email, userDto.Id, userDto.Website)) return false;
            var dbSet = InitialDbContext(userDto.Website);
            var list = await GetById(userDto.Id, userDto.Website);
            list.Add(userDto);
            var user = Mapper.Map<User>(userDto);
            dbSet.Attach(user);
            DbContext!.Entry(user).Property(u => u.UserName).IsModified = true;
            DbContext!.Entry(user).Property(u => u.ClassExpire).IsModified = true;
            DbContext!.Entry(user).Property(u => u.Class).IsModified = true;
            DbContext!.Entry(user).Property(u => u.NodeGroup).IsModified = true;
            DbContext!.Entry(user).Property(u => u.Money).IsModified = true;
            DbContext!.Entry(user).Property(u => u.ImValue).IsModified = true;
            DbContext!.Entry(user).Property(u => u.TransferEnable).IsModified = true;
            DbContext!.Entry(user).Property(u => u.RefBy).IsModified = true;
            DbContext!.Entry(user).Property(u => u.GroupExpire).IsModified = true;
            // dbSet.Update(user);
            if (await DbContext!.SaveChangesAsync() != 1) return false;
            EventCenter.Instance.Publish(new ModifyUserEvent()
            {
                Payload = list,
                Website = userDto.Website
            });
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> VerifyUser(string email, int userId, Website website)
    {
        var userBaseInfoDto = await GetUserBaseInfoById(userId, website);
        if (email == userBaseInfoDto.Email) return true;
        EventCenter.Instance.Publish(new IllegalOperationEvent()
        {
            Payload = new IllegalOperationPayload()
            {
                UserBaseInfo = userBaseInfoDto,
                Content = "企图通过修改请求用户Id参数来修改用户信息"
            }
        });
        return false;
    }

    private async Task<UserBaseInfoDto?> GetUserBaseInfoByEmail(string email, Website website)
    {
        var dbSet = InitialDbContext(website);
        var list = await dbSet
            .Where(it => it.Email == email)
            .Select(it => new UserBaseInfoDto()
            {
                Id = it.Id,
                Email = it.Email
            }).Take(1).ToListAsync();
        return list.Count == 0 ? null : list[0];
    }

    private async Task<UserBaseInfoDto> GetUserBaseInfoById(int id, Website website)
    {
        var dbSet = InitialDbContext(website);
        return await dbSet
            .Where(it => it.Id == id)
            .Select(it => new UserBaseInfoDto()
            {
                Id = it.Id,
                Email = it.Email
            }).FirstAsync();
    }

    private static Expression<Func<User, UserDto>> GetSelector(Website website)
    {
        return (p) => new UserDto()
        {
            Id = p.Id,
            UserName = p.UserName,
            Email = p.Email,
            ClassExpire = p.ClassExpire,
            NodeGroup = p.NodeGroup,
            Class = p.Class,
            T = p.T,
            U = p.U,
            D = p.D,
            Money = p.Money,
            ImValue = p.ImValue,
            TransferEnable = p.TransferEnable,
            GroupExpire = p.GroupExpire,
            RefBy = p.RefBy,
            Website = website
        };
    }
}