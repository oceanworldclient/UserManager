using AutoMapper;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Service;

public class ShopService : BaseService<Shop, ShopDto>
{
    public ShopService(IMapper mapper) : base(mapper)
    {
    }

    public async Task<IList<ShopDto>> GetShopsWithLimit(Website website)
    {
        switch (website)
        {
            case Website.World:
                return await GetByExpression(it => it.Id > 50 && it.Id < 68, website);
            case Website.Ocean:
                var idSet = new HashSet<long>()
                {
                    6, 8, 13, 14, 15, 16, 17, 18, 31, 32
                };
                return await GetByExpression(it => idSet.Contains(it.Id), website);
            case Website.Zebra:
                return await GetByExpression(it => it.Id > 15 && it.Id < 29, website);
            default:
                return new List<ShopDto>();
        }
    }
    
}