using System;

namespace Project.BuffSystem
{
    public interface IEffectEventContainer{
        void Add(Delegate callback);
        void Remove(Delegate callback);
        void Clear();
        void Trigger<TEventData>(TEventData data);
    }
    public interface IEffectEventListener
    {
        void AddListenerTo(EffectEventManager eventManager);
    }

   
    public enum EffectEventType{
        AddEffect,
        RemoveEffect,
        OnDamage,
        OnBeingDamage,
        OnKilled,
    }
}