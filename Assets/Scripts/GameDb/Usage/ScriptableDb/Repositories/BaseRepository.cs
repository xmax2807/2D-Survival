namespace Project.GameDb.ScriptableDatabase
{
    /// <summary>
    /// Base repository
    /// </summary>
    public abstract class BaseRepository<TEntity> : IReadDatabaseRepository<TEntity>
    {
        protected readonly ScriptableDatabase m_database;

        public BaseRepository(ScriptableDatabase database){
            m_database = database;
        }

        public abstract GameAsyncOperation<TEntity[]> GetAllEntities();
        public abstract GameAsyncOperation<TEntity> GetEntity(int id);
    }
}