using System.Collections;
using System.Threading.Tasks;

namespace Project.GameDb.ScriptableDatabase{
    public interface ISoundRepository{
        SoundData GetSound(int id);
    }
    internal class SoundRepository : BaseRepository<SoundData>, ISoundRepository
    {
        public SoundRepository(ScriptableDatabase database) : base(database)
        {
        }

        public SoundData GetSound(int id)
        {
            return m_database.GetSound(id);
        }
    }
}