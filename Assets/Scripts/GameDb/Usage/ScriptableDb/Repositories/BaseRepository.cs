namespace Project.GameDb.ScriptableDatabase
{
    /// <summary>
    /// Base repository
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly ScriptableDatabase m_database;

        public BaseRepository(ScriptableDatabase database){
            m_database = database;
        }
    }
}