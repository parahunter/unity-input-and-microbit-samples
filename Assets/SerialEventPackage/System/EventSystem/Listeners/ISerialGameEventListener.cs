using UnityEngine;
namespace MA.Events
{
    public interface ISerialGameEventListener<T0,T1>
    {
        void OnEventRaised(T0 identifier,T1 value);
        GameObject GetGameobject();
    }
}