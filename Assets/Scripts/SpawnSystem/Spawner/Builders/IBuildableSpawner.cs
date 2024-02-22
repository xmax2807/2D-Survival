namespace Project.SpawnSystem
{

    /// <summary>
    /// Make spawner buildable ability
    /// </summary>
    public interface IBuildableSpawner
    {
        void SetContext(ISpawnContext context);
        void AddSpawnEntities(uint[] id);
        void GetStorageRepositoryFrom(IStorageGetter storageGetter);
        void AddDropObservable(IDropObservable dropObservable);
        void AddDropFor(uint id, DropData dropData);
        ISpawner CastToSpawner();

        void AddSpawnLogicForPrepare(ISpawnLogic logic);
        void AddSpawnLogicForSpawn(ISpawnLogic logic);
    }
}