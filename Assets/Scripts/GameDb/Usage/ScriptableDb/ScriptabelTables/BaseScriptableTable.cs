using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase{
    public abstract class BaseScriptableTable<TData> : ScriptableObject{
        [SerializeField] protected TData[] tableOfData;
        private Dictionary<int, TData> m_table;

        protected abstract void GetIds(ref int[] ids);

        public IEnumerator LoadTable(){
            m_table = new Dictionary<int, TData>();
            int[] ids = new int[tableOfData.Length];
            GetIds(ref ids);

            for(int i = 0; i < ids.Length; ++i){
                m_table.Add(ids[i], tableOfData[i]);
            }

            yield return null;
        }
        public IEnumerator UnloadTable(){
            m_table = null;
            yield return null;
        }

        public Task<TData> GetEntity(int id){
            return Task.FromResult(m_table[id]);
        }
    }
}