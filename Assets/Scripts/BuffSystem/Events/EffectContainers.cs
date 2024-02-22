using System;

namespace Project.BuffSystem
{
    public class SingleEffectEventContainer<TData> : IEffectEventContainer{
        private event System.Action<TData> m_effectEvent;
        public void Add(Delegate callback){
            if(callback is System.Action<TData> action){
                m_effectEvent += action;
            }
        }

        public void Clear()
        {
            m_effectEvent = null;
        }

        public void Remove(Delegate callback){
            if(callback is System.Action<TData> action){
                m_effectEvent -= action;
            }
        }

        public void Trigger<TEventData>(TEventData data)
        {
            if(data is TData realData){
                m_effectEvent?.Invoke(realData);  
            }
        }
    }
}