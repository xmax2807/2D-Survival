namespace Project.SpawnSystem
{
    public interface IIndividualSpawnerBuilder
    {
        IBuildableSpawner Result {get;}
        IIndividualSpawnerBuilder AddSpawnEntities(IndividualData[] individualDatas, IStorageGetter storageGetter);
        IIndividualSpawnerBuilder AddDropObservable(IDropObservable dropObservable);
        IIndividualSpawnerBuilder Reset();
    }

    public interface ISpawnBuildDirector{
        ISpawner BuildSpawner(SpawnType spawnType, SpawnData data, IStorageGetter storageGetter);
        ICommandSelector BuildSpawnCommandSelector(SpawnScheduleData spawnScheduleData);
    }
}