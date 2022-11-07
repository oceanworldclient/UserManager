using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Server.Service;
using UserManager.Shared;
using UserManager.Shared.Request;

namespace UserManager.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "God")]
public class UserRoleController: ControllerBase
{
    
    private UserRoleService UserRoleService { get; }

    public UserRoleController(UserRoleService userRoleService)
    {
        UserRoleService = userRoleService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] AddRoleToUserDto toUserDto)
    {
        return Ok(await UserRoleService.AddRoleToUser(toUserDto));
    }
    
    [HttpPost]
    public async Task<IActionResult> FindUsers()
    {
        return Ok(await UserRoleService.FindAll());
    }
    
    [HttpPost]
    public async Task<IActionResult> FindUserByEmail(QueryIdentityUserDto dto)
    {
        return Ok(await UserRoleService.FindUserByEmail(dto));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteIdentityUserDto dto)
    {
        return Ok(await UserRoleService.DeleteUser(dto));
    }

}