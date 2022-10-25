using UserManager.Shared;

namespace UserManager.Blazor.State;

public class PanelState
{
    
    public Website Website { get; set; }

    public IList<UserDto> Users { get; set; } = new List<UserDto>();

    public QueryUserDto.QueryType QueryType = QueryUserDto.QueryType.Email;
    
    public UserDto? SelectedUser { get; set; }

}