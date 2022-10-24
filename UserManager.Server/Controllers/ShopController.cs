using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared;

namespace UserManager.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private ShopService ShopService { get; }

    public ShopController(ShopService service)
    {
        ShopService = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<IList<ShopDto>>> FindShop([FromBody] BaseDto baseDto)
    {
        return Ok(await ShopService.GetShopsWithLimit(baseDto.Website));
    }
}