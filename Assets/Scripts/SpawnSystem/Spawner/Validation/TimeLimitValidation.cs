using UnityEngine;

namespace Project.SpawnSystem
{
    public class DurationLimitValidation : ISpawnValidation
    {
        private float _duration;
        public bool Validate(IReadOnlySchedulerContext context)
        {
            return context.StartTime + _duration > Time.time;
        }
    }
}