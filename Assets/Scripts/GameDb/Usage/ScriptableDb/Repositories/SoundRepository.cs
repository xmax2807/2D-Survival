using System.Collections;
using System.Threading.Tasks;

namespace Project.GameDb.ScriptableDatabase{
    internal class SoundRepository : BaseRepository<SoundData>
    {
        public SoundRepository(ScriptableDatabase database) : base(database)
        {
        }

        public override GameAsyncOperation<SoundData[]> GetAllEntities()
        {
            throw new System.NotImplementedException();
        }

        public override GameAsyncOperation<SoundData> GetEntity(int id)
        {
            Task<SoundData> task = m_database.GetSound(id);
            IEnumerator operation = new GameDb.TaskYieldInstruction(task);
            return OperationManager.Instance.RequestOperation(operation, () => task.Result);
        }
    }
}