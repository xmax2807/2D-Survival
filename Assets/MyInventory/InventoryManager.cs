namespace MyInventory{
    internal class InventoryManager{
        readonly IInvetoryRepository _repository;
        public InventoryManager(IInvetoryRepository repository){
            _repository = repository;
        }
    }
}