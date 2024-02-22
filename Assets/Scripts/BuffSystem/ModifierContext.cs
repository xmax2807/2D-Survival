using System.Collections.Generic;
using UnityEngine;

namespace Project.BuffSystem
{
    public interface IModifierContext{
        float Duration{get;}
        bool IsActive{get;}
        bool IsConditionMet{get;}
        bool ValidateCondition(ITarget target);
        void OnModifierAddedTo(IEffectHandler target);
    }
    public class ModifierContext : IModifierContext{
        // Conditions to activate the effect
        // bool isActive
        // float Duration
        public float Duration {get; private set;}

        public bool IsActive => false;

        public bool IsConditionMet {get;private set;}

        public void OnModifierAddedTo(IEffectHandler target)
        {
            // condition can register some stuff like events, add component,... 
        }

        public bool ValidateCondition(ITarget target)
        {
            IsConditionMet = true;
            return true;
        }

        public void SetDuration(float duration){
            Duration = duration;
        }
    }
}