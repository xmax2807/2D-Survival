using System;
using System.Threading.Tasks;
using PlasticGui.WorkspaceWindow.BrowseRepository;

namespace MyInventory{
    public sealed class InventoryInitializer : IDisposable{
        public static bool IsInitialized{get; private set;}
        private static InventoryInitializer s_instance;

        private IInvetoryRepository m_repository;
        private int m_itemSlotCapacity;
        private IInventoryUI m_ui;
        private IInventoryEventMapper m_eventMapper;
        private InternalInventoryEventHandler m_eventHandler;
        private InventoryManager m_manager;

        public class InventoryInitializerBuilder{
            private readonly InventoryInitializer _instance;
            public InventoryInitializerBuilder(){
                _instance = new InventoryInitializer();
            }

            public void Reset(){
                _instance?.Reset();
            }

            public InventoryInitializer Build(){
                return _instance;
            }

            public InventoryInitializerBuilder WithItemSlotCapacity(int capacity){
                _instance.m_itemSlotCapacity = capacity;
                return this;
            }

            public InventoryInitializerBuilder WithRepository(IInvetoryRepository repository){
                _instance.m_repository = repository;
                return this;
            }

            public InventoryInitializerBuilder WithUI(IInventoryUI ui){
                _instance.m_ui = ui;
                return this;
            }

            public InventoryInitializerBuilder WithEventMapper(IInventoryEventMapper mapper){
                _instance.m_eventMapper = mapper;
                return this;
            }
        }

        private InventoryInitializer(){
            EnsureValidInitializer();
        }

        private InventoryInitializer(IInvetoryRepository repository, IInventoryUI ui, IInventoryEventMapper mapper) : this(){
            
            // if any of parameters is null, throw
            if(repository == null || ui == null || mapper == null){
                throw new ArgumentNullException("one or more paramters were null, can not create InventoryInitializer");
            }

            m_repository = repository;
            m_ui = ui;
            m_eventMapper = mapper;
        }


        /// <summary>
        /// used for builder, not for public
        /// </summary>
        private void Reset(){
            m_repository = null;
            m_itemSlotCapacity = 0;
            m_ui = null;
            m_eventMapper = null;
            m_eventHandler = null;
            m_manager = null;
        }

        public static InventoryInitializerBuilder CreateBuilder(){
            EnsureValidInitializer();

            return new InventoryInitializerBuilder();
        }

        public static InventoryInitializer Create(IInvetoryRepository repos, IInventoryUI ui, IInventoryEventMapper mapper){
            if(IsInitialized){
                return s_instance;
            }
            s_instance = new InventoryInitializer(repos, ui, mapper);
            return s_instance;
        }

        public void Intialize(){
            // GetDefaultIfNull<IInvetoryRepository>(ref _repository, () => new InventoryRepository());
            // GetDefaultIfNull<IInventoryUI>(ref _ui, () => new InventoryUI());
            // GetDefaultIfNull<IInventoryEventMapper>(ref _eventMapper, () => new InventoryEventMapper());
            if(IsInitialized){
                return;
            }

            m_manager = new InventoryManager(m_repository, m_ui);
            m_eventHandler = new InternalInventoryEventHandler(m_manager);
            m_eventMapper.AttachHandler(m_eventHandler);

            IsInitialized = true;
        }

        private void GetDefaultIfNull<T>(ref T value, in Func<T> creator){
            value ??= creator.Invoke();
        }

        public void Dispose()
        {
            m_eventMapper.Dispose();
            m_manager.Dispose();
        }

        private static void EnsureValidInitializer(){
            if(IsInitialized){
                throw new InvalidOperationException("InventoryInitializer has already been initialized.");
            }
        }
    }
}