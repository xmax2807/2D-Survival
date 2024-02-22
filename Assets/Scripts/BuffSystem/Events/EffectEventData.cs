
namespace Project.BuffSystem
{
    public class EffectAddedEventData{
        public ITarget Target {get;private set;}
        public int EffectId {get;private set;}

        public EffectAddedEventData(ITarget target, int effectId){
            Target = target;
            EffectId = effectId;
        }
    }

    public class DamageEventData{
        public ITarget Attacker {get;private set;}
        public ITarget Target {get;private set;}
        public int Amount {get;private set;}

        public DamageEventData(ITarget attacker, ITarget target, int amount){
            Attacker = attacker;
            Target = target;
            Amount = amount;
        }
    }

    public class KilledEventData{
        public ITarget Victim {get;private set;}
        public ITarget Target {get;private set;}

        public KilledEventData(ITarget victim, ITarget target){
            Victim = victim;
            Target = target;
        }
    }
}