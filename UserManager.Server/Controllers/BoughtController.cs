using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared;
using UserManager.Shared.Response;

namespace UserManager.Server.Controllers;

[Route("[controller]/[action]")]
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
    public async Task<ActionResult<BaseResult>> Update([FromBody] BoughtDto boughtDto)
    {
         var isSuccess = await BoughtService.Update(boughtDto, boughtDto.Website);
         return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

    public async Task<ActionResult<BaseResult>> BuyShop([FromBody] BoughtShopDto boughtShopDto)
    {
        return Ok(await BoughtService.BuyShop(boughtShopDto));
    }
    
}