namespace Project.GameDb
{
    public interface IDatabaseReposSubscription{
        void AddRepository<TRepository>(TRepository instance);
        void RemoveRepository<TRepository>();
    }
    public interface IDatabaseRepoProvider
    {
        TRepository GetRepository<TRepository>();
    }
}