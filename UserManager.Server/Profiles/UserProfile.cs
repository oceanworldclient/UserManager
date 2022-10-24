using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}