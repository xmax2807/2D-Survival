using System.Collections;
using System.Threading.Tasks;

namespace Project.GameDb.ScriptableDatabase
{
    public interface IVFXRepository{
        VFXData GetVFX(int id);
    }
    public class VFXRepository : BaseRepository<VFXData>, IVFXRepository
    {
        public VFXRepository(ScriptableDatabase database) : base(database)
        {
        }

        public VFXData GetVFX(int id) => m_database.GetVFX(id);
    }
}