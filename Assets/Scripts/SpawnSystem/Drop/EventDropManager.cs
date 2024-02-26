using System.Collections.Generic;
using Project.GameEventSystem;
using Project.Pooling;
using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "EventDropManager", menuName = "SpawnSystem/EventDropManager")]
    public class EventDropManager : ScriptableDropManager, IDropObservable
    {
        [SerializeField, EventID] int id_dropGoldEvent;
        [SerializeField, EventID] int id_dropEXPEvent;
        [SerializeField, EventID] int id_dropItemEvent;
        [SerializeField] ScriptableEventProvider m_eventProvider;
        [SerializeField] ScriptableRewardableEntityRegistry m_rewardableEntityRegistry;

        public override IDropObservable DropObservable => this;

        public void OnDrop(DropData data, int targetId)
        {
            var entity = m_rewardableEntityRegistry.GetById(targetId);
            if(entity == null) return;

            m_eventProvider.Invoke<int>(id_dropGoldEvent, (int)(data.GoldAmount * entity.GetGoldBonus()));
            m_eventProvider.Invoke<int>(id_dropEXPEvent, (int)(data.ExpAmount * entity.GetExpBonus()));
            m_eventProvider.Invoke(id_dropItemEvent, FilterItems(data.items, entity.GetDropBonus()));
        }

        private int[] FilterItems(in DropItem[] items, float dropBonus){
            List<int> result = QuickListPool<int>.GetList();

            for(int i = 0; i < items.Length; ++i){
                Rate rate = Random.Range(0, 101) + dropBonus;
                if(rate < items[i].rate){
                    result.Add(items[i].ItemId);
                }
            }

            int[] result_ids = result.ToArray();
            QuickListPool<int>.ReturnList(result);
            return result_ids;
        }
    }
}