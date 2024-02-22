using System;
using UnityEngine;
namespace Project.GameEventSystem
{
    [CreateAssetMenu(fileName = "EventSystem_EventProvider", menuName = "GameEventSystem/EventProvider")]
    public class ScriptableEventProvider : ScriptableObject, IEventProvider, IEventInvoker
    {
        [SerializeField] EventStorage m_eventStorage;
        public IEventSubscription GetEventSubscription(int id)
        {
            return m_eventStorage[id];
        }

        public void Invoke(int id)
        {
            m_eventStorage[id]?.Invoke();
        }

        public void Invoke<T>(int id, T data)
        {
            m_eventStorage[id]?.Invoke(data);
        }

        public void Invoke(int id, object data)
        {
            m_eventStorage[id]?.Invoke(data);
        }

        public void SubscribeToEvent(int id, Delegate callback)
        {
            m_eventStorage[id]?.Subscribe(callback);
        }

        public void UnsubscribeFromEvent(int id, Delegate callback)
        {
            m_eventStorage[id]?.Unsubscribe(callback);
        }
    }
}