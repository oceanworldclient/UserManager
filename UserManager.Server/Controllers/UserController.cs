using UserManager.Server.Service;
using UserManager.Shared.Response;

namespace UserManager.Server.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserManager.Shared;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private UserService UserService { get; }

    public UserController(UserService userService)
    {
        UserService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<IList<UserDto>>> FindUser([FromBody] QueryUserDto queryUserDto)
    {
        return queryUserDto.Type switch
        {
            QueryUserDto.QueryType.Id => Ok(await UserService.GetById(queryUserDto.Id, queryUserDto.Website)),
            QueryUserDto.QueryType.Email => Ok(await UserService.GetByEmail(queryUserDto.Email, queryUserDto.Website)),
            QueryUserDto.QueryType.Contact => Ok(await UserService.GetByContact(queryUserDto.Contact,
                queryUserDto.Website)),
            QueryUserDto.QueryType.Username => Ok(await UserService.GetByUserName(queryUserDto.UserName,
                queryUserDto.Website)),
            _ => Ok(new List<UserDto>())
        };
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult>> UpdateUser([FromBody] UserDto userDto)
    {
        var isSuccess = await UserService.ModifyUser(userDto);
        return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult>> ModifyPassword([FromBody] ModifyPasswordDto dto)
    {
        var isSuccess = await UserService.ModifyPassword(dto);
        return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

}