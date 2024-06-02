namespace MyInventory{
    public interface IInventoryUI{
        void Open(IInvetoryRepository repository);
        void Close(IInvetoryRepository repository);
    }
}