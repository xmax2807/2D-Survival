using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.BuffSystem
{
    /// <summary>
    /// This class acts as mediator between effects and EffectEventManager for specific targets
    /// </summary>
    public class TargetEffectEventManager
    {
        private readonly Dictionary<EffectEventType, Dictionary<ITarget, List<Delegate>>> m_subscribers;
        
        public static TargetEffectEventManager Create(EffectEventManager effectEventManager){
            if(effectEventManager == null){
                throw new ArgumentNullException(nameof(effectEventManager));
            }

            var subscribers = EffectEventHelper.CreateDictionaryBaseOnType((EffectEventType type) => new Dictionary<ITarget, List<Delegate>>());
            var result = new TargetEffectEventManager(subscribers);
            result.RegisterThisTo(eventManager: effectEventManager);
            return result;
        }
        private TargetEffectEventManager(Dictionary<EffectEventType, Dictionary<ITarget, List<Delegate>>> subscribers){
            m_subscribers = subscribers;
        }

        private void RegisterThisTo(EffectEventManager eventManager){
            if(eventManager == null){
                return;
            }
            eventManager.AddListener<EffectAddedEventData>(EffectEventType.AddEffect, OnAddEffect);
            eventManager.AddListener<EffectAddedEventData>(EffectEventType.RemoveEffect, OnRemoveEffect);
            eventManager.AddListener<DamageEventData>(EffectEventType.OnDamage, OnDamage);
            eventManager.AddListener<DamageEventData>(EffectEventType.OnBeingDamage, OnBeingDamage);
            eventManager.AddListener<KilledEventData>(EffectEventType.OnKilled, OnKilled);
        }

        public void AddSubscriber<TEventData>(ITarget target, EffectEventType type, Action<ITarget,TEventData> callback){
            if(target == null){
                return;
            }
            Dictionary<ITarget, List<Delegate>> targetSubscribers = m_subscribers[type];
            if (!targetSubscribers.ContainsKey(target)){
                targetSubscribers.Add(target, new List<Delegate>());
            }
            targetSubscribers[target].Add(callback);
        }

        public void RemoveSubscriber<TEventData>(ITarget target, EffectEventType type, Action<ITarget,TEventData> callback){
            if(target == null){
                return;
            }
            Dictionary<ITarget, List<Delegate>> targetSubscribers = m_subscribers[type];
            if(!targetSubscribers.ContainsKey(target)){
                return;
            }
            targetSubscribers[target].Remove(callback);
        }

        private void InvokeEventToListeners<TEventData>(ITarget target, EffectEventType type, TEventData data){
            Dictionary<ITarget, List<Delegate>> targetSubscribers = m_subscribers[type];
            if(!targetSubscribers.ContainsKey(target)){
                return;
            }
            List<Delegate> subscribers = targetSubscribers[target];

            for(int i = subscribers.Count - 1; i >= 0; --i){
                (subscribers[i] as Action<ITarget, TEventData>)?.Invoke(target, data);
            }
        }

        #region Event Handlers
        private void OnKilled(KilledEventData data)
        {
            InvokeEventToListeners(data.Target, EffectEventType.OnKilled, data);
        }

        private void OnBeingDamage(DamageEventData data)
        {
            InvokeEventToListeners(data.Target, EffectEventType.OnBeingDamage, data);
        }

        private void OnDamage(DamageEventData data)
        {
            InvokeEventToListeners(data.Target, EffectEventType.OnDamage, data);
        }

        private void OnRemoveEffect(EffectAddedEventData data)
        {
            InvokeEventToListeners(data.Target, EffectEventType.RemoveEffect, data);
        }

        private void OnAddEffect(EffectAddedEventData data)
        {
            InvokeEventToListeners(data.Target, EffectEventType.AddEffect, data);
        }
        #endregion
    }
}