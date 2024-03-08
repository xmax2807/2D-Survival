using System.Collections;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase
{
    [CreateAssetMenu(fileName = "AnimatorEffectTable", menuName = "ScriptableDatabase/AnimatorEffectTable")]
    public class AnimatorEffectTable : BaseScriptableTable<AnimatorEffectData>
    {
        [SerializeField] private AnimatorEffectData[] m_effectTable;
        public override IEnumerator LoadTable(){
            m_table??= new System.Collections.Generic.Dictionary<int, AnimatorEffectData>();
            for(int i = 0; i < m_effectTable.Length; i++){
                m_table.Add(m_effectTable[i].id, m_effectTable[i]);
            }
            yield return null;
        }
    }
}