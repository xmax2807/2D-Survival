namespace MyInventory{
    public interface IInventoryUI{
        void Open(InventoryContext context);
        void Close();
    }
}