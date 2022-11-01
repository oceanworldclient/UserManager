using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "God,User")]
public class ShopController : ControllerBase
{
    private ShopService ShopService { get; }

    public ShopController(ShopService service)
    {
        ShopService = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<IList<ShopDto>>> QueryShop([FromBody] QueryShopDto baseDto)
    {
        return Ok(await ShopService.GetShopsWithLimit(baseDto.Website));
    }
}