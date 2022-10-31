using System.Collections.Concurrent;
using System.ComponentModel.Design.Serialization;
using Microsoft.AspNetCore.Identity;

namespace UserManager.Server.EventHub;

public class EventHub
{
    private ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> EventHandlers { get; } = new();

    private EventHub()
    {
    }

    public static EventHub Instance { get; } = new();

    public void Publish<T>(T e)
    {
        if (!EventHandlers.ContainsKey(e.GetType())) return;
        foreach (var dic in EventHandlers[e.GetType()])
        {
            ((AbsentEventHandler<T>)dic.Value).Handle(e);
        }
    }

    public void Register<T>(Type type, AbsentEventHandler<T> eventHandler)
    {
        if (!EventHandlers.ContainsKey(type)) EventHandlers.TryAdd(type, new ConcurrentDictionary<string, object>());
        EventHandlers[type].TryAdd(eventHandler.GetType().FullName!, eventHandler);
    }

    public void UnRegister<T>(Type type, AbsentEventHandler<T> eventHandler)
    {
        if (EventHandlers.ContainsKey(type))
        {
            EventHandlers[type].TryRemove(eventHandler.GetType().FullName!, out _);
        }
    }
    
}