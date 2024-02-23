using Project.GameEventSystem;
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

        public override IDropObservable DropObservable => this;

        public void OnDrop(DropData data, int targetId)
        {
            m_eventProvider.Invoke(id_dropGoldEvent, data.GoldAmount);
            m_eventProvider.Invoke(id_dropEXPEvent, data.ExpAmount);
            m_eventProvider.Invoke(id_dropItemEvent, data.GetItemIds());
        }
    }
}