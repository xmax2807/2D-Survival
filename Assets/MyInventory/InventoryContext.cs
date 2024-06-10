namespace MyInventory{
    public enum InventoryActionType : byte{
        Buy,
        Sell,
        Consume
    }
    public sealed class InventoryContext {

        private readonly InternalInventoryManager m_manager;
        internal InventoryContext(InternalInventoryManager manager){
            m_manager = manager;
        }
        public InventoryItemDetail GetItemDetail(int itemId){
            return m_manager.GetItemDetail(itemId);
        }

        public InventorySlotItem[] GetAllItems(){
            return m_manager.GetAllItems();
        }

        public UnityEngine.Sprite GetItemIcon(ushort iconId){
            return m_manager.GetIcon(iconId);
        }

        public void ConsumeItem(int itemId, int amount){
            m_manager.ModifyItem(itemId, amount);
        }

        public void SellItem(int itemId, int amount){
            throw new System.NotImplementedException();
        }

        public void BuyItem(int itemId, int amount){
            throw new System.NotImplementedException();
        }
    }
}