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
        switch (queryUserDto.Type)
        {
            case QueryUserDto.QueryType.Id:
                IList<UserDto> res = new List<UserDto>();
                var userDto = await UserService.GetById(queryUserDto.Id, queryUserDto.Website);
                if (userDto != null) res.Add(userDto);
                return Ok(res);
            case QueryUserDto.QueryType.Email:
                return Ok(await UserService.GetByEmail(queryUserDto.Email, queryUserDto.Website));
            case QueryUserDto.QueryType.Contact:
                return Ok(await UserService.GetByContact(queryUserDto.Contact, queryUserDto.Website));
            case QueryUserDto.QueryType.Username:
                return Ok(await UserService.GetByUserName(queryUserDto.UserName, queryUserDto.Website));
            default:
                return Ok(new List<UserDto>());
        }
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult>> UpdateUser([FromBody] UserDto userDto)
    {
        var isSuccess = await UserService.Update(userDto, userDto.Website);
        return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult>> ModifyPassword([FromBody] ModifyPasswordDto dto)
    {
        var isSuccess = await UserService.ModifyPassword(dto);
        return Ok(new BaseResult() { IsSuccess = isSuccess });
    }

}