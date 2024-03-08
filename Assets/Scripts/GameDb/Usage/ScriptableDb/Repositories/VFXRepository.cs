namespace Project.GameDb.ScriptableDatabase
{
    public interface IVFXRepository{
        ParticleEffectData GetParticleEffect(int id);
        AnimatorEffectData GetAnimatorEffect(int id);
    }
    public class VFXRepository : BaseRepository, IVFXRepository
    {
        public VFXRepository(ScriptableDatabase database) : base(database)
        {
        }

        public AnimatorEffectData GetAnimatorEffect(int id) => m_database.GetAnimatorEffect(id);
        public ParticleEffectData GetParticleEffect(int id) => m_database.GetParticleEffect(id);
    }
}