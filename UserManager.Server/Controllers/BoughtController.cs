using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared;
using UserManager.Shared.Request;
using UserManager.Shared.Response;

namespace UserManager.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "God,User")]
public class BoughtController : ControllerBase
{
    private BoughtService BoughtService { get; }

    public BoughtController(BoughtService boughtService)
    {
        BoughtService = boughtService;
    }

    [HttpPost]
    public async Task<ActionResult<IList<BoughtDto>>> QueryBoughtByUserId([FromBody] QueryBoughtDto query)
    {
        return Ok(await BoughtService.GetLastTenByUserId(query.UserId, query.Website));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult>> Update([FromBody] BoughtDto boughtDto)
    {
         var isSuccess = await BoughtService.Update(boughtDto, boughtDto.Website);
         return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

    public async Task<ActionResult<BaseResult>> BuyShop([FromBody] BuyShopDto buyShopDto)
    {
        return Ok(await BoughtService.BuyShop(buyShopDto));
    }
    
    public async Task<ActionResult<BaseResult>> Upgrade([FromBody] BuyShopDto buyShopDto)
    {
        return Ok(await BoughtService.Upgrade(buyShopDto));
    }
    
    public async Task<ActionResult<BaseResult>> DeleteBought([FromBody] DeleteBoughtDto delete)
    {
        return Ok(await BoughtService.DeleteById(delete));
    }
    
    public async Task<ActionResult<BaseResult>> CloseRenew([FromBody] CloseRenewDto closeRenewDto)
    {
        return Ok(await BoughtService.CloseRenew(closeRenewDto));
    }
    
    public async Task<ActionResult<BaseResult>> RestoreBought([FromBody] RestoreBoughtDto restoreBoughtDto)
    {
        return Ok(await BoughtService.RestoreBought(restoreBoughtDto));
    }

}