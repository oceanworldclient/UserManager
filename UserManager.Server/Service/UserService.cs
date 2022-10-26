using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Service;

public class UserService : BaseService<User, UserDto>
{
    public UserService(IMapper mapper) : base(mapper)
    {
    }

    public new async Task<UserDto?> GetById(int id, Website website)
    {
        var userDto = await base.GetById(id, website);
        return userDto ?? null;
    }

    public async Task<IList<UserDto>> GetByEmail(string email, Website website)
    {
        return await GetByExpression(it => it.Email == email, website);
    }

    public async Task<IList<UserDto>> GetByContact(string contact, Website website)
    {
        return await GetByExpression(it => it.ImValue != null && it.ImValue.Contains(contact), website);
    }

    public async Task<IList<UserDto>> GetByUserName(string username, Website website)
    {
        return await GetByExpression(it => it.UserName.Contains(username), website);
    }

    public async Task<bool> ModifyPassword(ModifyPasswordDto dto)
    {
        var user = new User()
        {
            Id = dto.Id,
            Pass = dto.NewPassword
        };
        return await Update(user, dto.Website);
    }
    
}