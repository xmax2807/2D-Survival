using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.GameDb.ScriptableDatabase{
    public abstract class BaseScriptableTable<TData> : ScriptableObject{
        protected Dictionary<int, TData> m_table;
        public abstract IEnumerator LoadTable();
        public virtual IEnumerator UnloadTable(){
            m_table = null;
            yield return null;
        }

        public Task<TData> GetEntity(int id){
            return Task.FromResult(m_table[id]);
        }
    }
}