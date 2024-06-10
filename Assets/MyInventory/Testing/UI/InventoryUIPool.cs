using UnityEngine;
namespace MyInventory.Testing
{
    public class InventoryUIPool : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI slotPrefab;
        [SerializeField] private int InitSlotCount = 16;
        private PoolHolder<InventorySlotUI> m_slotUIHolder;

        void Awake(){
            if(slotPrefab != null){
                m_slotUIHolder = CreateHolder("Slot Holder", InitSlotCount, slotPrefab);
            }
        }

        

        public InventorySlotUI RentSlotUI(){
            if(m_slotUIHolder == null){
                return null;
            }
            return m_slotUIHolder.Get();
        }

        public void ReturnSlotUI(InventorySlotUI slot){
            if(m_slotUIHolder == null){
                return;
            }
            m_slotUIHolder.Return(slot);
        }

        private PoolHolder<TComponent> CreateHolder<TComponent>(string name, int preInitCount, TComponent prefab) where TComponent : Component
        {
            Transform transform = new GameObject(name).transform;
            transform.SetParent(this.transform, false);
            PoolHolder<TComponent> result = new(transform);
            PopulateObject(result, preInitCount, prefab);

            return result;
        }

        private void PopulateObject<TComponent>(PoolHolder<TComponent> holder, int count, TComponent prefab) where TComponent : Component{
            for(int i = 0; i < count; ++i){
                TComponent obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                holder.AddChild(obj);
            }
        }

        private System.Collections.IEnumerator PopulateObjectIn<TComponent>(PoolHolder<TComponent> holder, int count, TComponent prefab) where TComponent : Component{
            for(int i = 0; i < count; ++i){
                TComponent obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                holder.AddChild(obj);

                // for every 4th object
                if((i & 3) == 0){
                    yield return null;
                }
            }
        }


        private class PoolHolder<TComponent> where TComponent : Component{
            readonly Transform m_holder;
            private int m_capacity;

            public PoolHolder(Transform holder){
                m_holder = holder;
            }

            public void AddChild(TComponent child){
                if(child.gameObject.activeSelf){
                    child.gameObject.SetActive(false);
                }
                child.transform.SetParent(m_holder, false);
                ++m_capacity;
            }

            public void Destroy(){
                for (int i = 0; i < m_capacity; ++i){
                    // destroy child
                    Object.Destroy(m_holder.GetChild(i).gameObject);
                }
                m_capacity = 0;
            }

            internal TComponent Get()
            {
                if(m_holder.childCount == 0){
                    return null;
                }
                TComponent result = m_holder.GetChild(0).GetComponent<TComponent>();
                return result;
            }

            internal void Return(TComponent slot)
            {
                if(m_holder.childCount >= m_capacity){
                    return;
                }
                slot.gameObject.SetActive(false);
                slot.transform.SetParent(m_holder, false);
            }
        }
    }
}