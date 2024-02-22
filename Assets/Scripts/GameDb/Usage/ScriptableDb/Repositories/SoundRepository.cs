namespace Project.GameDb.ScriptableDatabase{
    internal class SoundRepository : BaseRepository<SoundData>
    {
        public SoundRepository(ScriptableDatabase database) : base(database)
        {
        }

        public override GameAsyncOperation<SoundData[]> GetAllEntities()
        {
            throw new System.NotImplementedException();
        }

        public override GameAsyncOperation<SoundData> GetEntity(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}