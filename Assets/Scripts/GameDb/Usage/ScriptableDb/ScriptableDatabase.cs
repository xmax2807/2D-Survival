using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
namespace Project.GameDb.ScriptableDatabase{
    [CreateAssetMenu(fileName = "Database", menuName = "ScriptableDatabase/Database")]
    public class ScriptableDatabase : ScriptableObject{
        [SerializeField] BaseScriptableTable<SoundData> m_soundTable;
        [SerializeField] BaseScriptableTable<VFXData> m_vfxTable;

        public IEnumerator Initialize(){
            if(m_soundTable != null){
                yield return m_soundTable.LoadTable();
            }
            if(m_vfxTable != null){
                yield return m_vfxTable.LoadTable();
            }
        }

        public SoundData GetSound(int id){
            return m_soundTable.GetEntity(id);
        }
        public VFXData GetVFX(int id){
            return m_vfxTable.GetEntity(id);
        }
    }
}