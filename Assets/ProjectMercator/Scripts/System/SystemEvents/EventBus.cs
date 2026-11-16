using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _listeners = new();

    // Подписка
    public static void Subscribe<T>(Action<T> listener)
    {
        var type = typeof(T);
        if (_listeners.ContainsKey(type))
        {
            _listeners[type] = Delegate.Combine(_listeners[type], listener);
        }
        else
        {
            _listeners[type] = listener;
        }
    }

    // Отписка
    public static void Unsubscribe<T>(Action<T> listener)
    {
        var type = typeof(T);
        if (_listeners.ContainsKey(type))
        {
            _listeners[type] = Delegate.Remove(_listeners[type], listener);

            // Если слушателей не осталось, удаляем ключ
            if (_listeners[type] == null)
            {
                _listeners.Remove(type);
            }
        }
    }

    // Сюда суём класс с данными буквально EventBus.Publish(new UIEvent { bool = true}) - for example
    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (_listeners.TryGetValue(type, out Delegate del))
        {
            (del as Action<T>)?.Invoke(eventData);
        }
    }
}
