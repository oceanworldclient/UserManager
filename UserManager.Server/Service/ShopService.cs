﻿using System.Collections.Concurrent;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.Model;
using UserManager.Shared;

namespace UserManager.Server.Service;

public class ShopService : BaseService<Shop, ShopDto>
{
    public static ConcurrentDictionary<Website, ConcurrentDictionary<long, ShopDto>> ShopDic => new();

    public static ConcurrentDictionary<Website, ConcurrentDictionary<long, ShopDto>> AllShopDic => new();
    public ShopService(IMapper mapper) : base(mapper)
    {
    }

    public async Task<IList<ShopDto>> GetShopsWithLimit(Website website)
    {
        if (ShopDic.ContainsKey(website)) return ShopDic[website].Values.ToList();
        IList<ShopDto> res;
        switch (website)
        {
            case Website.World:
                res = await GetByExpression(it => it.Id > 50 && it.Id < 68, website);
                break;
            case Website.Ocean:
                var idSet = new HashSet<long>()
                {
                    6, 8, 13, 14, 15, 16, 17, 18, 31, 32
                };
                res = await GetByExpression(it => idSet.Contains(it.Id), website);
                break;
            case Website.Zebra:
                res = await GetByExpression(it => it.Id > 15 && it.Id < 29, website);
                break;
            default:
                res = new List<ShopDto>();
                break;
        }

        var dic = new ConcurrentDictionary<long, ShopDto>();
        foreach (var shopDto in res)
        {
            dic.TryAdd(shopDto.Id, shopDto);
        }

        ShopDic.TryAdd(website, dic);
        return res;
    }

    public async Task<IDictionary<long, ShopDto>> GetAllShops(Website website)
    {
        
        if (AllShopDic.ContainsKey(website)) return AllShopDic[website];
        var dbSet = InitialDbContext(website);
        var shops = await dbSet.ToListAsync();
        ConcurrentDictionary<long, ShopDto> dic = new();
        foreach (var shop in shops)
        {
            dic.TryAdd(shop.Id, Mapper.Map<ShopDto>(shop));
        }

        return dic;
    }

}