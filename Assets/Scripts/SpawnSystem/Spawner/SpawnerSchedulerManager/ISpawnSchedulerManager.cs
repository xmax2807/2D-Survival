using System.Threading.Tasks;

namespace Project.SpawnSystem
{
    public interface ISpawnSchedulerManager
    {
        Task<ISpawnScheduler[]> GetAllSpawnSchedulerAsync();
    }
}