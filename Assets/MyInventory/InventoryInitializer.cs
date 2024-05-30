namespace MyInventory{
    public sealed class InventoryInitializer{
        private IInvetoryRepository _repository;
        private int _itemSlotCapacity;
        private InventoryManager _inventoryManager;
        

        public class InventoryInitializerBuilder{
            private InventoryInitializer _instance;
            public InventoryInitializerBuilder(){
                _instance = new InventoryInitializer();
            }

            public void Reset(){
                _instance = new InventoryInitializer();
            }

            public InventoryInitializer Build(){
                return _instance;
            }

            public InventoryInitializerBuilder WithItemSlotCapacity(int capacity){
                
                return this;
            }

            public InventoryInitializerBuilder WithRepository(IInvetoryRepository repository){
                _instance._repository = repository;
                return this;
            }
        }

        private InventoryInitializer(){}

        public static InventoryInitializerBuilder Create(){
            return new InventoryInitializerBuilder();
        }

        public void Intialize(){
            _inventoryManager = new InventoryManager(_repository);
        }
    }
}