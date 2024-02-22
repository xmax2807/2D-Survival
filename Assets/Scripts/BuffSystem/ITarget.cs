using System.Collections.Generic;
using UnityEngine;

namespace Project.BuffSystem
{
    public interface ITarget
    {
        IStatModifier StatModifier { get; } // additional stats for modifying
        IStatGetter BaseStateGetter { get; } // original stats before effect applied
        IEffectHandler EffectHandler { get; }
    }

    public interface IEffectHandler {
        void AddEffectModifier(IEffectModifier modifier, ITarget source);
        void RemoveEffectModifier(IEffectModifier modifier, ITarget source);
        void Update();
    }

/// <summary>
/// This class will be managed by handler when an modifier is added
/// </summary>
    public class ModifierEntry{

        public ModifierEntry(EffectModifierData data, float time, ITarget source = null)
        {
            Source = source;
            ModifierData = data;
            StartTime = time;
        }

        // this can be null if modifier is gained from events (Quest, Skill)
        // use for some complex modifiers like: Debuff with stats bonus from source before deal damage to target
        public ITarget Source {get;private set;}
        public float StartTime {get;private set;}
        public EffectModifierData ModifierData {get;private set;}

        public void ActivateModifierTo(ITarget target){
            for(int i = ModifierData.Effects.Length - 1; i >= 0; --i){
                ModifierData.Effects[i].Apply(target);
            }
        }

        public void DeactivateModifierTo(ITarget target){
            for(int i = ModifierData.Effects.Length - 1; i >= 0; --i){
                ModifierData.Effects[i].Remove(target);
            }
        }

        public bool IsExpired(float currentTime){
            // No duration on this modifier
            if(ModifierData.Context.Duration <= 0) return false;

            return currentTime > StartTime + ModifierData.Context.Duration;
        }
    }

    public class EffectHandler : IEffectHandler
    {
        private readonly ITarget m_target;
        private readonly List<ModifierEntry> m_modifiers = new List<ModifierEntry>();

        public void AddEffectModifier(IEffectModifier modifier, ITarget source)
        {
            m_modifiers.Add(new ModifierEntry(modifier.GetData(), Time.time, source));
            //TODO: notify other modifiers about changes when apply this modifier
            Update();
        }

        public void RemoveEffectModifier(IEffectModifier modifier, ITarget source)
        {
            //m_modifiers.Remove(modifier);
            for(int i = m_modifiers.Count - 1; i >= 0; --i){
                if(m_modifiers[i].ModifierData == modifier.GetData() && m_modifiers[i].Source == source){
                    m_modifiers.RemoveAt(i);
                }
            }
            Update();
            //TODO: notify other modifiers about changes when remove this modifier
        }

        public void Update()
        {
            for (int i = m_modifiers.Count - 1; i >= 0; --i){
                // check duration if need
                ModifierEntry currentMod = m_modifiers[i];
                IModifierContext context = currentMod.ModifierData.Context;
                
                bool isConditionMet = context.ValidateCondition(m_target);
                // continue if this modifier is not active and condition is not met
                if(context.IsActive == false && isConditionMet == false) continue;
                
                // if condition is met and this modifier is not active
                if(isConditionMet && context.IsActive == false){
                    currentMod.ActivateModifierTo(m_target);
                }

                // if this modifier has duration
                // check if expired for removal
                if(currentMod.IsExpired(Time.time)){
                    m_modifiers.RemoveAt(i);
                }

                // if
            }
        }
    }
}