namespace Project.GameDb
{
    public interface IDatabaseRepoProvider
    {
        IReadDatabaseRepository<SoundData> SoundRepository {get;}
        IReadDatabaseRepository<VFXData> VFXRepository {get;}
    }
}