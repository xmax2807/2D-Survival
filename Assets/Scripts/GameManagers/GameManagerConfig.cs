using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Project.BuffSystem;
using Project.GameEvent;
using Project.GameEventSystem;
using Project.InputHandler;
using Project.LOD;
using Project.LootSystem;
using UnityEngine;

namespace Project.Manager
{
    public partial class GameManager
    {
        #region Effect Config
        private ScriptableEffectConfiguration m_effectSystemConfiguration;
        public IEffectEventPublisher EffectEventPublisher => m_effectSystemConfiguration;
        public TargetEffectEventManager TargetEffectEventManager { get; private set; }
        #endregion

        #region Game Event Config
        public IGameEventRegister GameEventRegisterer => _gameEventService;
        public IGameEventPublisher GameEventPublisher => _gameEventService;
        private GameEventService _gameEventService;

        private ScriptableEventProvider _gameEventProvider;
        public IEventInvoker GameEventInvoker => _gameEventProvider;
        public IEventProvider GameEventProvider => _gameEventProvider;

        private IEventAPI _gameEventAPI;
        public static IEventAPI GameEventAPI => _instance._gameEventAPI;
        #endregion

        #region LOD Config
        private VisibleRendererStorage m_visibleRendererStorage;
        public IRendererRegisterer StorageRendererRegisterer => m_visibleRendererStorage;
        public ICentralizedRenderer StorageRendererPublisher => m_visibleRendererStorage;
        #endregion

        #region Utils
        public Queue<IEnumerator> CoroutineCommandQueue { get; private set; }
        #endregion

        #region Parameters
        private GameParams.IParamProvider _params;
        public static GameParams.IParamProvider Params => _instance._params;
        #endregion

        #region Database
        private GameDb.ScriptableDatabase.ScriptableDatabaseRepoProvider m_scriptableDatabase;
        public static GameDb.IDatabaseRepoProvider RepoProvider => _instance.m_scriptableDatabase;
        #endregion

        #region LootSystem
        private LootSystemConfiguration m_lootSystemConfiguration;
        public static ILootSystemAPI LootSystem => _instance.m_lootSystemConfiguration.LootSystem;
        #endregion

        [SerializeField] InputHandler_InputSystem m_inputSystem;
        public IInputHandler InputHandler => m_inputSystem;

        void OnEnable()
        {

            // Init Coroutines
            Utils.Coroutines.Initialize(this);
            CoroutineCommandQueue = new Queue<IEnumerator>();
            StartCoroutine(RunQueueCommands());
            //

            _gameEventProvider = Resources.Load<GameEventSystem.ScriptableEventProvider>("EventSystem_EventProvider");
            _gameEventAPI = Resources.Load<GameEventAPI>("EventSystem_GameEventAPI");
            DefineEventHandlers();

            InitializeDatabase(filePath: "ScriptableDatabaseRepoProvider");
            InitializeLootSystem(filePath: "LootSystemConfiguration");

            GetParams();

            m_effectSystemConfiguration = Resources.Load<ScriptableEffectConfiguration>("EffectConfiguration");


            m_visibleRendererStorage = ScriptableObject.CreateInstance<VisibleRendererStorage>();
            TargetEffectEventManager = TargetEffectEventManager.Create(m_effectSystemConfiguration.EventManager);
        }

        private void InitializeLootSystem(string filePath)
        {
            m_lootSystemConfiguration = Resources.Load<LootSystemConfiguration>(filePath);
            m_lootSystemConfiguration.Initialize(this.transform);
        }

        private void InitializeDatabase(string filePath)
        {
            m_scriptableDatabase = Resources.Load<GameDb.ScriptableDatabase.ScriptableDatabaseRepoProvider>(filePath);
            CoroutineCommandQueue.Enqueue(m_scriptableDatabase.Initialize());
        }

        private IEnumerator RunQueueCommands()
        {
            while (true)
            {
                if (CoroutineCommandQueue.Count > 0)
                {
                    yield return CoroutineCommandQueue.Dequeue();
                }
                else
                {
                    yield return null;
                }
            }
        }

        void DefineEventHandlers()
        {
            _gameEventService = new GameEventService();
            _gameEventService.AddHandler(new SoundEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new PhysicEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new ItemEventHandler(_gameEventAPI));
            _gameEventService.AddHandler(new ItemDropEventHandler(_gameEventAPI));
        }
        void GetParams()
        {
            GameParams.ScriptableParamProvider _LoadParams = Resources.Load<GameParams.ScriptableParamProvider>("ParamProvider");
            _LoadParams.Initialize();
            _params = _LoadParams;
        }
    }
}