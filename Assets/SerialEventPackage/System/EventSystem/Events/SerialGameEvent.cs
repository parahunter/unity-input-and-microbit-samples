using System.Collections.Generic;
using UnityEngine;

namespace MA.Events
{

    public abstract class SerialGameEvent<T0,T1> : ScriptableObject
    {
        private readonly List<ISerialGameEventListener<T0,T1>> eventListeners = new List<ISerialGameEventListener<T0,T1>>();
        private T0 lastIdentifier;
        private T1 lastValue;

        public void Raise(T0 identifier, T1 value)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(identifier,value);
            }
            lastIdentifier = identifier;
            lastValue = value;
        }

        public void RegisterListener(ISerialGameEventListener<T0,T1> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(ISerialGameEventListener<T0,T1> listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }

        public List<ISerialGameEventListener<T0,T1>> GetListeners()
        {
            return eventListeners;
        }

        public T1 GetLastValue()
        {
            return lastValue;
        }
    }
}
