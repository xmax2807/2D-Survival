using System;

namespace Project.GameEventSystem
{
    public class SoundEventSubscription : IEventController
    {
        event Action<int> m_soundEvent;
        event Action<SoundEventData> m_soundEventFull;
        public void Invoke()
        {
            //Do nothing
        }

        public void Invoke<T>(T data)
        {
            if(data is int id){
                m_soundEvent?.Invoke(id);
            }
            else if(data is SoundEventData eventData){
                m_soundEventFull?.Invoke(eventData);
            }
        }

        public void Invoke(object data)
        {
            if(data is int id){
                m_soundEvent?.Invoke(id);
            }
            else if(data is SoundEventData eventData){
                m_soundEventFull?.Invoke(eventData);
            }
        }

        public void Subscribe(Delegate callback)
        {
            if(callback is Action<int> action)
            {
                m_soundEvent += action;
            }
            else if(callback is Action<SoundEventData> full_callback){
                m_soundEventFull += full_callback;
            }
        }

        public void Unsubscribe(Delegate callback)
        {
            if(callback is Action<int> action){
                m_soundEvent -= action;
            }
            else if(callback is Action<SoundEventData> full_callback){
                m_soundEventFull -= full_callback;
            }
        }
    }
}