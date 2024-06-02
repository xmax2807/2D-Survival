using System;

namespace MyInventory{

    internal sealed class InternalInventoryEventHandler : IInventoryEventHandler
    {
        private readonly InventoryManager m_manager;

        public InternalInventoryEventHandler(InventoryManager manager)
        {
            m_manager = manager;
        }

        public void OnAttachedToMapper(IInventoryEventMapper mapper)
        {
            mapper.SubscribeToEvent<InventoryItemEventData>(HandleItemEvent);
            mapper.SubscribeToEvent<InventoryUIActiveEventData>(HandleUIActiveEvent);
        }

        public void OnDetachedFromMapper(IInventoryEventMapper mapper)
        {
            mapper.UnsubscribeFromEvent<InventoryItemEventData>(HandleItemEvent);
            mapper.UnsubscribeFromEvent<InventoryUIActiveEventData>(HandleUIActiveEvent);
        }

        #region Event Handling
        private void HandleItemEvent(InventoryItemEventData data)
        {
            m_manager.ModifyItem(data.ItemId, data.Amount);
        }
        
        private void HandleUIActiveEvent(InventoryUIActiveEventData data){
            m_manager.SetActiveInventory(data.IsActive);
        }
        #endregion
    }
    internal sealed class InventoryManager : IDisposable{
        const int DefaultItemSlotCapacity = 32;

        readonly IInvetoryRepository m_repository;
        readonly IInventoryUI m_ui;
        readonly int m_itemSlotCapacity;
        
        public InventoryManager(IInvetoryRepository repository, IInventoryUI ui, int itemSlotCapacity = DefaultItemSlotCapacity){
            m_repository = repository;
            m_ui = ui;
            m_itemSlotCapacity = itemSlotCapacity;
        }

        public void SetActiveInventory(bool isActive){

            if(isActive){
                m_ui.Open(m_repository);
            }
            else{
                m_ui.Close(m_repository);
            }
        }

        public void ModifyItem(int itemId, int count){
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            m_ui?.Close(m_repository);
        }
    }
}