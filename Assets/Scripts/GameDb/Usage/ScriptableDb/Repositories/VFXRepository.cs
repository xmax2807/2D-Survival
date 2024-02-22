namespace Project.GameDb.ScriptableDatabase
{
    public class VFXRepository : BaseRepository<VFXData>
    {
        public VFXRepository(ScriptableDatabase database) : base(database)
        {
        }

        public override GameAsyncOperation<VFXData[]> GetAllEntities()
        {
            throw new System.NotImplementedException();
        }

        public override GameAsyncOperation<VFXData> GetEntity(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}