using UserManager.Shared;

namespace UserManager.Blazor.State;

public class StateManager
{
    private List<PanelState> _states = new()
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