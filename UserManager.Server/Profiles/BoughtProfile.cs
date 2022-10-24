using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Profiles;

public class BoughtProfile : Profile
{
    public BoughtProfile()
    {
        CreateMap<Bought, BoughtDto>().ReverseMap();
    }
}