namespace Project.SpawnSystem
{
    public class TotalCountLimitValidation : ISpawnValidation
    {
        private readonly uint _limit;
        public TotalCountLimitValidation(uint limit){
            _limit = limit;
        }
        public bool Validate(IReadOnlySchedulerContext context)
        {
            return context.TotalSpawned < _limit;
        }
    }

    public class ActiveCountLimitValidation : ISpawnValidation
    {
        private readonly uint _limit;
        public ActiveCountLimitValidation(uint limit){
            _limit = limit;
        }
        public bool Validate(IReadOnlySchedulerContext context)
        {
            return context.ActiveSpawnedCount < _limit;
        }
    }
}