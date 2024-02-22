using System;
using System.Collections.Generic;

namespace Project.BuffSystem
{
    public interface IEffectEventPublisher{
        void Publish<TEventData>(EffectEventType type, ref TEventData eventData);
    }
    public class EffectEventManager: IEffectEventPublisher
    {
        private Dictionary<EffectEventType, IEffectEventContainer> m_eventMap;
        //TODO: add factory to get effect event container
        private IEffectEventFactory m_eventFactory;

        public EffectEventManager(IEffectEventFactory eventFactory){
            if(eventFactory == null){
                throw new ArgumentNullException(nameof(eventFactory));
            }
            m_eventFactory = eventFactory;
            m_eventMap = new Dictionary<EffectEventType, IEffectEventContainer>(){
                {EffectEventType.AddEffect, m_eventFactory.CreateAddEffectEvent()},
                {EffectEventType.RemoveEffect, m_eventFactory.CreateRemoveEffectEvent()},
                {EffectEventType.OnDamage, m_eventFactory.CreateOnDamageEvent()},
                {EffectEventType.OnBeingDamage, m_eventFactory.CreateOnBeingDamageEvent()},
                {EffectEventType.OnKilled, m_eventFactory.CreateOnKilledEvent()},
            };
        }

        public void AddListener<TEventData>(EffectEventType type, System.Action<TEventData> callback)
        {
            if(!m_eventMap.ContainsKey(type)){
                // throw or return null
                return;
            }
            m_eventMap[type].Add(callback);
        }
        public void RemoveListener<TEventData>(EffectEventType type, System.Action<TEventData> callback)
        {
            if(!m_eventMap.ContainsKey(type)){
                // throw or return null
                return;
            }
            m_eventMap[type].Remove(callback);
        }

        public void Publish<TEventData>(EffectEventType type, ref TEventData eventData){
            if(!m_eventMap.ContainsKey(type)){
                // throw or return null
                return;
            }

            m_eventMap[type].Trigger<TEventData>(eventData);
        }
    }
}