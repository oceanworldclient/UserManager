using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;
using System.Linq.Expressions;

namespace UserManager.Server.Service;

public class UserService : BaseService<User, UserDto>
{
    public UserService(IMapper mapper) : base(mapper)
    {
    }

    public new async Task<IList<UserDto>> GetById(int id, Website website)
    {
        return await GetByExpression(it => it.Id == id, GetSelector(website), website);
    }

    public async Task<IList<UserDto>> GetByEmail(string email, Website website)
    {
        return await GetByExpression(it => it.Email == email, GetSelector(website) ,website);
    }

    public async Task<IList<UserDto>> GetByContact(string contact, Website website)
    {
        return await GetByExpression(it => it.ImValue != null && it.ImValue.Contains(contact), GetSelector(website) ,website);
    }

    public async Task<IList<UserDto>> GetByUserName(string username, Website website)
    {
        return await GetByExpression(it => it.UserName.Contains(username), GetSelector(website) ,website);
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        var dbSet = InitialDbContext(dto.Website);
        var user = new User()
        {
            Id = dto.Id,
            Pass = dto.NewPassword
        };
        dbSet.Attach(user);
        DbContext!.Entry(user).Property(u => u.Pass).IsModified = true;
        dbSet.Update(user);
        return await DbContext!.SaveChangesAsync() == 1;
    }

    public async Task<bool> ModifyUser(UserDto userDto)
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