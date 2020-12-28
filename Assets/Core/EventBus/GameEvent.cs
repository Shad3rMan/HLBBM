using System;
using System.Collections.Generic;

namespace Core.EventBus
{
    public class GameEvent<T>
    {
        private readonly List<Action<T>> _listeners;

        public GameEvent()
        {
            _listeners = new List<Action<T>>(32);
        }

        public void AddListener(Action<T> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void Invoke(T argument)
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke(argument);
            }
        }
    }

    public class GameEvent
    {
        private readonly List<Action> _listeners;

        public GameEvent()
        {
            _listeners = new List<Action>();
        }
        
        public void AddListener(Action listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(Action listener)
        {
            _listeners.Remove(listener);
        }

        public void Invoke()
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke();
            }
        }
    }
}