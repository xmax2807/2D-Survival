
using System;

namespace Project.GameEventSystem
{
    /// <summary>
    /// Handle item added to inventory from various sources: mission reward, enemy defeat, chests, etc
    /// </summary>
    public class ItemEventHandler : EventHandler
    {
        readonly Action m_onItemAddedToInventoryEmpty;
        readonly Action<InventoryItemEventData> m_onItemAddedToInventory;
        readonly Action m_onItemRemovedFromInventoryEmpty;
        readonly Action<InventoryItemEventData> m_onItemRemovedFromInventory;
        public ItemEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            m_onItemAddedToInventoryEmpty = OnItemAdded;
            m_onItemAddedToInventory = OnItemAdded_Data;
            m_onItemRemovedFromInventoryEmpty = OnItemRemoved;
            m_onItemRemovedFromInventory = OnItemRemoved_Data;
        }

        public override void RegisterToAPI()
        {
            //TODO subscribe to item received
            m_eventAPI.InventoryItemAddedEvent.Subscribe(m_onItemAddedToInventoryEmpty);
            m_eventAPI.InventoryItemAddedEvent.Subscribe(m_onItemAddedToInventory);

            m_eventAPI.InventoryItemRemovedEvent.Subscribe(m_onItemRemovedFromInventory);
            m_eventAPI.InventoryItemRemovedEvent.Subscribe(m_onItemRemovedFromInventoryEmpty);
        }

        public override void UnregisterFromAPI(){
            m_eventAPI.InventoryItemAddedEvent.Unsubscribe(m_onItemAddedToInventoryEmpty);
            m_eventAPI.InventoryItemAddedEvent.Unsubscribe(m_onItemAddedToInventory);

            m_eventAPI.InventoryItemRemovedEvent.Unsubscribe(m_onItemRemovedFromInventory);
            m_eventAPI.InventoryItemRemovedEvent.Unsubscribe(m_onItemRemovedFromInventoryEmpty);
        }

        #region Handle Events
        void OnItemAdded(){}
        void OnItemAdded_Data(InventoryItemEventData data){}
        void OnItemRemoved(){}
        void OnItemRemoved_Data(InventoryItemEventData data){}
        #endregion
    }
}