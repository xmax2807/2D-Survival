namespace Project.SpawnSystem
{
    public interface IReadOnlySchedulerContext{
        float StartTime {get;}
        uint TotalSpawned {get;}
        uint ActiveSpawnedCount {get;}
    }
    public class SchedulerContext : IReadOnlySchedulerContext
    {
        public float StartTime {get; set;}
        public uint TotalSpawned {get; set;}
        public uint ActiveSpawnedCount {get; set;}
    }
}