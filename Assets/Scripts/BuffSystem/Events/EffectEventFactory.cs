using System.Collections.Generic;
using Unity.VisualScripting;

namespace Project.BuffSystem
{
    public interface IEffectEventFactory{
        IEffectEventContainer CreateAddEffectEvent();
        IEffectEventContainer CreateRemoveEffectEvent();
        IEffectEventContainer CreateOnDamageEvent();
        IEffectEventContainer CreateOnBeingDamageEvent();
        IEffectEventContainer CreateOnKilledEvent();
    }
    public class EffectEventFactory : IEffectEventFactory
    {
        public IEffectEventContainer CreateAddEffectEvent()
        {
            return new SingleEffectEventContainer<EffectAddedEventData>();
        }

        public IEffectEventContainer CreateOnBeingDamageEvent()
        {
            return new SingleEffectEventContainer<DamageEventData>();
        }

        public IEffectEventContainer CreateOnDamageEvent()
        {
            return new SingleEffectEventContainer<DamageEventData>();
        }

        public IEffectEventContainer CreateOnKilledEvent()
        {
            return new SingleEffectEventContainer<KilledEventData>();
        }

        public IEffectEventContainer CreateRemoveEffectEvent()
        {
            return new SingleEffectEventContainer<EffectAddedEventData>();
        }
    }
}