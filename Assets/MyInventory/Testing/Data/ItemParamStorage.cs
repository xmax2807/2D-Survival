using System.Collections.Generic;
using UnityEngine;

namespace MyInventory.Testing{
    [CreateAssetMenu(fileName = "ItemParamStorage", menuName = "MyInventory/InventoryStorage/ItemParamStorage")]
    public class ItemParamStorage : ScriptableObject, IStorage<ushort, ItemData>
    {
        [SerializeField] ItemData[] m_datas;
        Dictionary<ushort, ItemData> m_cache;

        void OnEnable(){
            if(m_cache == null){
                m_cache = new Dictionary<ushort, ItemData>();
                for(int i = 0; i < m_datas.Length; ++i){
                    m_cache.TryAdd(m_datas[i].Id, m_datas[i]);
                }
            }
        }
        void OnDisable(){
            if(m_cache != null){
                m_cache = null;
            }
        }

        public ItemData GetValue(ushort id)
        {
            m_cache.TryGetValue(id, out ItemData value);
            return value;   
        }
    }
}