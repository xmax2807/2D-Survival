using System.Collections;
using System.Threading.Tasks;

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
            Task<VFXData> task = m_database.GetVFX(id);
            IEnumerator operation = new TaskYieldInstruction(task);
            return OperationManager.Instance.RequestOperation(operation, ()=>task.Result);
        }
    }
}