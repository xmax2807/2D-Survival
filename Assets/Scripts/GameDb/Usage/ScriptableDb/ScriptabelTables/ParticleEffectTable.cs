using System.Collections;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    [CreateAssetMenu(fileName = "ParticleEffectTable", menuName = "ScriptableDatabase/ParticleEffectTable")]
    public class ParticleEffectTable : BaseScriptableTable<ParticleEffectData>
    {
        [SerializeField] ParticleEffectData[] tableOfData;
        public override IEnumerator LoadTable()
        {
            m_table ??= new System.Collections.Generic.Dictionary<int, ParticleEffectData>();
            foreach (var data in tableOfData){
                m_table.Add(data.id, data);
            }
            yield return null;
        }
    }
}