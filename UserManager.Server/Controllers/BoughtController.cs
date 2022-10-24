using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using UserManager.Server.Service;
using UserManager.Shared;

namespace UserManager.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class BoughtController : ControllerBase
{
    private BoughtService BoughtService { get; }

    public BoughtController(BoughtService boughtService)
    {
        BoughtService = boughtService;
    }

    [HttpPost]
    public async Task<ActionResult<IList<BoughtDto>>> FindShop([FromBody] QueryBoughtDto query)
    {
        return Ok(await BoughtService.GetLastTenByUserId(query.UserId, query.Website));
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Update([FromBody] BoughtDto boughtDto)
    {
        return Ok(await BoughtService.Update(boughtDto, boughtDto.Website));
    }

    public async Task<ActionResult<BaseResult>> BuyShop([FromBody] BoughtShopDto boughtShopDto)
    {
        return Ok(await BoughtService.BuyShop(boughtShopDto));
    }
    
}