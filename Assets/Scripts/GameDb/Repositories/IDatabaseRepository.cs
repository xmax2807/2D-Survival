namespace Project.GameDb
{
    public interface IReadDatabaseRepository
    {
        GameAsyncOperation<T> GetEntity<T>(int id);
        GameAsyncOperation<T[]> GetAllEntities<T>();
    }

    public interface IReadDatabaseRepository<TEntity>{
        GameAsyncOperation<TEntity> GetEntity(int id);
        GameAsyncOperation<TEntity[]> GetAllEntities();
    }
}