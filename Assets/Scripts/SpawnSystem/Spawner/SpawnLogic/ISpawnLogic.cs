using System.Threading.Tasks;

namespace Project.SpawnSystem
{
    public interface ISpawnLogic
    {
        Task<bool> PerformLogic(ISpawnContext context);
    }
}