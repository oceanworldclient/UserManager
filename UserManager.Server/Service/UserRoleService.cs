using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.EntityFramework;
using UserManager.Server.Utils;
using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Server.Service;

public class UserRoleService
{
    private UserDbContext UserDbContext { get; }

    public UserRoleService(UserDbContext userDbContext)
    {
        UserDbContext = userDbContext;
    }

    public async Task<List<IdentityUserDto>> FindAll()
    {
        var userList = await UserDbContext.Users.Select(it => new IdentityUserDto()
        {
            UserName = it.UserName,
            Email = it.Email,
            Id = it.Id
        }).ToListAsync();
        return await HandlerRole(userList);
    }

    private async Task<List<IdentityUserDto>> HandlerRole(List<IdentityUserDto> userDtos)
    {
        var hashSet = userDtos.Select(it=>it.Id).ToHashSet();
        var listAsync = await UserDbContext.UserRoles.Where(it=>hashSet.Contains(it.UserId)).ToListAsync();
        var roleSet = listAsync.Select(it=>it.RoleId).ToHashSet();
        var roleDict = await UserDbContext.Roles.Where(it => roleSet.Contains(it.Id)).ToDictionaryAsync(it => it.Id);
        var dic = new Dictionary<string, string>();
        foreach (var identityUserRole in listAsync)
        {
            dic.Add(identityUserRole.UserId, roleDict[identityUserRole.RoleId].Name);
        }

        foreach (var user in userDtos)
        {
            user.Role = dic[user.Id];
        }

        return userDtos;
    }

    public async Task<List<IdentityUserDto>> FindUserByEmail(QueryIdentityUserDto dto)
    {
        return await UserDbContext.Users.Where(it=>it.Email==dto.Email).Select(it => new IdentityUserDto()
        {
            UserName = it.UserName,
            Email = it.Email,
            Id = it.Id
        }).Take(1).ToListAsync();
    }

    public async Task<bool> DeleteUser(DeleteIdentityUserDto dto)
    {
        var user = new IdentityUser() { Id = dto.Id };
        UserDbContext.Attach(user);
        UserDbContext.Users.Remove(user);
        return (await UserDbContext.SaveChangesAsync()) == 1;
    }

    public async Task<bool> AddRoleToUser(AddRoleToUserDto toUserDto)
    {
        var email = toUserDto.Email.Decrypt();
        var roleName = toUserDto.Role.Decrypt();
        var user = await UserDbContext.Users.Where(it => it.Email == email).FirstAsync();
        var role = await UserDbContext.Roles.Where(it => it.Name == roleName).FirstAsync();
        var listAsync = await UserDbContext.UserRoles.Where(it => it.UserId == user.Id).ToListAsync();
        var tmp = new IdentityUserRole<string>();
        if (listAsync.Count != 0)
        {
            UserDbContext.UserRoles.Remove(listAsync[0]);
        }
        tmp.UserId = user.Id;
        tmp.RoleId = role.Id;
        await UserDbContext.AddAsync(tmp);
        return await UserDbContext.SaveChangesAsync() >= 1;
    }

}