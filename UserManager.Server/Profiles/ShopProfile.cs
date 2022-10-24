using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Profiles;

public class ShopProfile : Profile
{
    public ShopProfile()
    {
        CreateMap<Shop, ShopDto>().ReverseMap();
    }
}