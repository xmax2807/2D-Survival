using System.Collections;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    [CreateAssetMenu(fileName = "SoundTable", menuName = "ScriptableDatabase/SoundTable", order = 1)]
    public class SoundTable : BaseScriptableTable<SoundData>
    {
        [SerializeField] SoundData[] tableOfData;

        public override IEnumerator LoadTable()
        {
            m_table ??= new System.Collections.Generic.Dictionary<int, SoundData>();
            foreach (var data in tableOfData){
                m_table.Add(data.SoundId, data);
            }

            yield return null;
        }
    }
}