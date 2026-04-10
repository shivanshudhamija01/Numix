using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, Delegate> _events = new();
    public void Subscribe<T>(Action<T> listener) where T : IGameEvent
    {
        var type = typeof(T);
        if (!_events.ContainsKey(type)) _events[type] = null;
        _events[type] = Delegate.Combine(_events[type], listener);
    }
    public void Unsubscribe<T>(Action<T> listener) where T : IGameEvent
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var listeners))
        {
            var result = Delegate.Remove(listeners, listener);
            if (result == null)
            {
                _events.Remove(type);
            }
            else
            {
                _events[type] = result;
            }
        }
    }
    public void Publish<T>(T gameEvent) where T : IGameEvent
    {
        var type = typeof(T);
        if (_events.TryGetValue(type, out var listener))
        {
            var callback = listener as Action<T>;
            callback?.Invoke(gameEvent);
        }
    }


}
