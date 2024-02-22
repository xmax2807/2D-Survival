namespace Project.SpawnSystem
{
    public interface ISpawnValidation
    {
        bool Validate(IReadOnlySchedulerContext context);
    }
}