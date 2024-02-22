using UnityEngine;

namespace Project.SpawnSystem
{
    /// <summary>
    /// this condition will prevent scheduler spawning (stop or pause)
    /// </summary>
    [System.Serializable]
    public class SpawnCondition{
        public enum ConditionType : byte{
            LimitDuration,
            LimitTotalCount,
            LimitActveCount
        }

        [SerializeField] private ConditionType conditionType;
        public ConditionType SpawnConditionType => conditionType;
        [SerializeField] private float value;
        public float Value => value;
    }

    [System.Serializable]
    public class SpawnScheduleData
    {
        public enum SpawnTriggerType
        {
            TimeBased,
            EventBased,
        }
        [SerializeField] private SpawnCondition[] stopConditions;
        public SpawnCondition[] StopConditions => stopConditions;
        [SerializeField] private SpawnTriggerType spawnTriggerType;
        public SpawnTriggerType TriggerType => spawnTriggerType;
        [SerializeField] private float spawnTime;
        public float SpawnTime => spawnTime;
        [SerializeField] private bool hasRepeat;
        public bool HasRepeat => hasRepeat;
        [SerializeField] private float intervalTime;
        public float IntervalTime => intervalTime;
        [SerializeField] private SpawnCondition repeatCondition;
        public SpawnCondition RepeatCondition => repeatCondition;
    }
}