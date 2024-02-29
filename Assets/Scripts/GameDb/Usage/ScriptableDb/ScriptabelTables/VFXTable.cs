using System.Collections;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    [CreateAssetMenu(fileName = "VFXTable", menuName = "ScriptableDatabase/VFXTable")]
    public class VFXTable : BaseScriptableTable<VFXData>
    {
        [SerializeField] VFXData[] tableOfData;
        public override IEnumerator LoadTable()
        {
            m_table ??= new System.Collections.Generic.Dictionary<int, VFXData>();
            foreach (var data in tableOfData){
                m_table.Add(data.id, data);
            }
            yield return null;
        }
    }
}