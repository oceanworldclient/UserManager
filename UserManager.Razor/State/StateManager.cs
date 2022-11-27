using UserManager.Shared;
using UserManager.Shared.Enum;

namespace UserManager.Razor.State;

public class StateManager
{
    private readonly List<PanelState> _states = new()
    {
        new PanelState() { Website = Website.World },
        new PanelState() { Website = Website.Ocean },
        new PanelState() { Website = Website.Zebra }
    };
    
    public PanelState GetState(int i)
    {
        return _states[i];
    }
}