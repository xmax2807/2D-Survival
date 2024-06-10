using System.Collections.Generic;
using UnityEngine;

namespace MyInventory.Testing{
    public class ExampleRepository : IInventoryRepository
    {
        private readonly IStringStorage m_itemNameStorage, m_itemDescriptionStorage;
        private readonly IStorage<ushort, ItemData> m_itemParamStorage;
        private readonly IIconStorage m_itemIconStorage;
        private readonly PlayerInventory _inventoryStorage;

        internal ExampleRepository(IStringStorage itemNameStorage, 
            IStringStorage itemDescriptionStorage, 
            IStorage<ushort, ItemData> itemParamStorage,
            IIconStorage itemIconStorage,
            PlayerInventory inventory
        )
        {
            // if any of this is null throw
            if(itemNameStorage == null 
            || itemDescriptionStorage == null 
            || itemParamStorage == null 
            || itemIconStorage == null 
            || inventory == null){
                throw new System.ArgumentNullException("one or more paramters were null, can not create ExampleRepository");
            }

            m_itemNameStorage = itemNameStorage;
            m_itemDescriptionStorage = itemDescriptionStorage;
            m_itemParamStorage = itemParamStorage;
            m_itemIconStorage = itemIconStorage;
            _inventoryStorage = inventory;
        }

        public InventoryItemDTO[] GetAllItems()
        {
            InventoryItemDTO[] result = new InventoryItemDTO[_inventoryStorage.Count];

            for(int i = 0; i < result.Length; ++i){
                var itemData = m_itemParamStorage.GetValue((ushort)_inventoryStorage[i].ItemId);
                if(itemData == null){
                    result[i] = InventoryItemDTO.NullItem;
                }
                else{
                    result[i] = new InventoryItemDTO(itemData.Id, _inventoryStorage[i].Count, itemData.SellValue, itemData.IconId);
                }
            }

            return result;
        }

        public string GetItemDescription(int id)
        {
            return m_itemDescriptionStorage.GetValue((ushort)id);
        }

        public Sprite GetItemIcon(ushort id)
        {
            return m_itemIconStorage.GetValue(id);
        }

        public string GetItemName(int id)
        {
            return m_itemNameStorage.GetValue((ushort)id);
        }
    }
}