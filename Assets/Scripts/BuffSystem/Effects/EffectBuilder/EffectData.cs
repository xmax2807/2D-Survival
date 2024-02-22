using System;
using UnityEngine;
using UnityEngine.Events;

namespace Project.BuffSystem
{
    public partial class EffectData : ScriptableObject
    {
        public enum EffectType
        {
            ModifyStat,
            RemoveOtherEffect,
            AddOtherEffect
        }
        [System.Serializable]
        public class DeactivateCondition{
            public enum ConditionType : byte{
                None, // no condition
                UseDuration,
                OnEvent
            }
            public ConditionType Type;
            public float Duration; // Use when condition type is UseDuration
            public EffectEventType EventType;
            
            public UnityEvent OnDeactivate;
        }
        [System.Serializable]
        public class TimeTick
        {
            public float Interval;
            public UnityEvent OnTick;
        }
        [System.Serializable]
        public class ModifyStatData
        {
            public enum OperationType{Plus, Mul}
            public OperationType Operation;
            public StatType StatType;
            public float Amount;
        }
        public EffectType Type;
        public bool HasDuration => DeactivateConditionData.Type == DeactivateCondition.ConditionType.UseDuration;
        public bool HasTickTime;
        public TimeTick Tick;
        public DeactivateCondition DeactivateConditionData;
        public ModifyStatData ModifyStat;
        public EffectData[] Children;

        public bool HasChildren => Children != null && Children.Length > 0;
    }
}