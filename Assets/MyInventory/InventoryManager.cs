using System;
using UnityEngine;

namespace MyInventory{

    internal sealed class InternalInventoryEventHandler : IInventoryEventHandler
    {
        private readonly InternalInventoryManager m_manager;
        private InventoryContext m_context;

        public InternalInventoryEventHandler(InternalInventoryManager manager)
        {
            m_manager = manager;
            m_context = new InventoryContext(manager);
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
            m_manager.SetActiveInventory(data.IsActive, m_context);
        }
        #endregion
    }
    internal sealed class InternalInventoryManager : IDisposable{
        const int DefaultItemSlotCapacity = 32;

        readonly IInventoryRepository m_repository;
        readonly IInventoryUI m_ui;
        readonly int m_itemSlotCapacity;
        private InventoryItemDTO[] m_itemDTOs;
        
        public InternalInventoryManager(IInventoryRepository repository, IInventoryUI ui, int itemSlotCapacity = DefaultItemSlotCapacity){
            m_repository = repository;
            m_ui = ui;
            m_itemSlotCapacity = itemSlotCapacity;
        }

        public void ModifyItem(int itemId, int count){
            throw new System.NotImplementedException();
        }

        private InventoryItemDetail m_detail;
        public InventoryItemDetail GetItemDetail(int itemId){
            InventoryItemDTO item = GetItemDTO(itemId);
            m_detail ??= new InventoryItemDetail();

            m_detail.ItemID = itemId;
            m_detail.Icon = m_repository.GetItemIcon(item.IconID);
            m_detail.Description = m_repository.GetItemDescription(itemId);
            m_detail.Name = m_repository.GetItemName(itemId);

            return m_detail;
        }

        private InventoryItemDTO GetItemDTO(int itemId){
            if(itemId == -1){
                return InventoryItemDTO.NullItem;
            }
            for(int i = 0; i < m_itemDTOs.Length; ++i){
                if(m_itemDTOs[i].ItemID == itemId){
                    return m_itemDTOs[i];
                }
            }

            return InventoryItemDTO.NullItem;
        }

        internal void SetActiveInventory(bool isActive, InventoryContext context)
        {
            if(m_ui == null || context == null) return;

            if(isActive){
                m_ui.Open(context);
            }
            else{
                m_ui.Close();
            }
        }

        public void Dispose()
        {
            Array.Clear(m_itemDTOs, 0, m_itemDTOs.Length);
            m_itemDTOs = null;
            m_detail = null;

            m_ui?.Close();
        }

        internal InventorySlotItem[] GetAllItems()
        {
            m_itemDTOs ??= m_repository.GetAllItems();

            InventorySlotItem[] result = new InventorySlotItem[m_itemDTOs.Length];
            for(int i = 0; i < m_itemDTOs.Length; ++i){
                result[i] = new InventorySlotItem(m_itemDTOs[i].ItemID, m_itemDTOs[i].ItemCount, m_itemDTOs[i].IconID);
            }
            return result;
        }

        internal Sprite GetIcon(ushort iconId)
        {
            return m_repository.GetItemIcon(iconId);
        }
    }

}