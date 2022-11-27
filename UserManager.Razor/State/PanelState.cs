using UserManager.Shared;
using UserManager.Shared.Enum;

namespace UserManager.Razor.State;

public class PanelState
{
    
    public Website Website { get; set; }

    public IList<UserDto> Users { get; set; } = new List<UserDto>();

    public QueryUserDto.QueryType QueryType = QueryUserDto.QueryType.Email;

    public string SearchString { get; set; } = "";

}