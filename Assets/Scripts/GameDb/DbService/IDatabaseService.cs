using System.Collections;
using System.Threading.Tasks;

namespace Project.GameDb
{
    /// <summary>
    /// Interface access the whole Database
    /// </summary>
    public interface IDatabaseService
    {
        IEnumerator Initialize();
        IReadDatabaseRepository GetRepository();
    }
}