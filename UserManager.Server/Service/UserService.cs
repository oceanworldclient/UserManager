using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.Utils;

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
        finally
        {
            Finish();
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
        finally
        {
            Finish();
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
        finally
        {
            Finish();
        }
    }

    public async Task<IList<UserDto>> GetByUserName(string username, Website website)
    {
        try
        {
            return await GetByExpression(it => it.UserName.Contains(username) && it.IsAdmin == 0, GetSelector(website),
                website);
        }
        catch
        {
            return new List<UserDto>();
        }
        finally
        {
            Finish();
        }
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        try
        {
            var dbSet = InitialDbContext(dto.Website);
            HandlerPassword(dto);
            var user = new User()
            {
                Id = dto.UserId,
                Pass = dto.NewPassword
            };
            dbSet.Attach(user);
            DbContext!.Entry(user).Property(u => u.Pass).IsModified = true;
            return await DbContext!.SaveChangesAsync() == 1;
        }
        catch
        {
            return false;
        }
        finally
        {
            Finish();
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
                dto.NewPassword = MD5Utils.Encrypt(dto.NewPassword, "");
                return;
            default:
                return;
        }
    }

    public async Task<bool> ModifyUser(UserDto userDto)
    {
        try
        {
            var dbSet = InitialDbContext(userDto.Website);
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
            return await DbContext!.SaveChangesAsync() == 1;
        }
        catch
        {
            return false;
        }
        finally
        {
            Finish();
        }
    }

    public async Task<bool> VerifyUser(UserDto userDto)
    {
        var userBase = await GetUserBaseInfoById(userDto.Id, userDto.Website);
        if (userDto.Email != userBase.Email)
        {
            return false;
        }
        return true;
    }

    private async Task<UserBaseInfoDto> GetUserBaseInfoById(int id, Website website)
    {
        var dbSet = InitialDbContext(website);
        return await dbSet.Select(it => new UserBaseInfoDto()
        {
            Id = it.Id,
            Email = it.Email
        }).Where(it => it.Id == id).FirstAsync();
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