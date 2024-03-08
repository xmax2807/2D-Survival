using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
namespace Project.GameDb.ScriptableDatabase{
    [CreateAssetMenu(fileName = "Database", menuName = "ScriptableDatabase/Database")]
    public class ScriptableDatabase : ScriptableObject{
        [SerializeField] BaseScriptableTable<SoundData> m_soundTable;
        [SerializeField] BaseScriptableTable<ParticleEffectData> m_particleEffectTable;
        [SerializeField] BaseScriptableTable<AnimatorEffectData> m_animatorEffectTable;

        public IEnumerator Initialize(){
            if(m_soundTable != null){
                yield return m_soundTable.LoadTable();
            }
            if(m_particleEffectTable != null){
                yield return m_particleEffectTable.LoadTable();
            }
            if(m_animatorEffectTable != null){
                yield return m_animatorEffectTable.LoadTable();
            }
        }

        public SoundData GetSound(int id){
            return m_soundTable.GetEntity(id);
        }
        public ParticleEffectData GetParticleEffect(int id){
            return m_particleEffectTable.GetEntity(id);
        }

        public AnimatorEffectData GetAnimatorEffect(int id) => m_animatorEffectTable.GetEntity(id);
    }
}