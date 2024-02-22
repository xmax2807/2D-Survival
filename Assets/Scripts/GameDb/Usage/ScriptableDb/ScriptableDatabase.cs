using System.Threading.Tasks;
using UnityEngine;
namespace Project.GameDb.ScriptableDatabase{
    [CreateAssetMenu(fileName = "Database", menuName = "ScriptableDatabase/Database")]
    public class ScriptableDatabase : ScriptableObject{
        [SerializeField] BaseScriptableTable<SoundData> m_soundTable;
        [SerializeField] BaseScriptableTable<VFXData> m_vfxTable;

        public Task<SoundData> GetSound(int id){
            return m_soundTable.GetEntity(id);
        }
        public Task<VFXData> GetVFX(int id){
            return m_vfxTable.GetEntity(id);
        }
    }
}