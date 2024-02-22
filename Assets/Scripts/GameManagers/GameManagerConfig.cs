using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Project.BuffSystem;
using Project.GameEvent;
using Project.GameEventSystem;
using Project.InputHandler;
using Project.LOD;
using UnityEngine;

namespace Project.Manager
{
    public partial class GameManager
    {
        #region Effect Config
        private ScriptableEffectConfiguration m_effectSystemConfiguration;
        public IEffectEventPublisher EffectEventPublisher => m_effectSystemConfiguration;
        public TargetEffectEventManager TargetEffectEventManager {get;private set;}
        #endregion

        #region Game Event Config
        public IGameEventRegister GameEventRegisterer => _gameEventService;
        public IGameEventPublisher GameEventPublisher => _gameEventService;
        private GameEventService _gameEventService;

        private ScriptableEventProvider _gameEventStorage;
        public IEventInvoker GameEventInvoker => _gameEventStorage;
        public IEventProvider GameEventProvider => _gameEventStorage;

        private IEventAPI _gameEventAPI;
        public static IEventAPI GameEventAPI => _instance._gameEventAPI;
        #endregion

        #region LOD Config
        private VisibleRendererStorage m_visibleRendererStorage;
        public IRendererRegisterer StorageRendererRegisterer => m_visibleRendererStorage;
        public ICentralizedRenderer StorageRendererPublisher => m_visibleRendererStorage;
        #endregion

        #region Utils
        public Queue<IEnumerator> CoroutineCommandQueue {get;private set;}
        #endregion

        [SerializeField] InputHandler.InputHandler_InputSystem m_inputSystem;
        public IInputHandler InputHandler => m_inputSystem;

        void OnEnable(){
            _gameEventService = new GameEventService();
            _gameEventStorage = Resources.Load<GameEventSystem.ScriptableEventProvider>("EventSystem_EventProvider");
            _gameEventAPI = Resources.Load<GameEventAPI>("EventSystem_GameEventAPI");
            m_effectSystemConfiguration = Resources.Load<ScriptableEffectConfiguration>("EffectConfiguration");
            m_visibleRendererStorage = ScriptableObject.CreateInstance<VisibleRendererStorage>();
            TargetEffectEventManager = TargetEffectEventManager.Create(m_effectSystemConfiguration.EventManager);
            CoroutineCommandQueue = new Queue<IEnumerator>();
        }

        void Start(){
            Utils.Coroutines.Initialize(this);
            StartCoroutine(RunQueueCommands());
        }

        private IEnumerator RunQueueCommands()
        {
            while(true){
                if(CoroutineCommandQueue.Count > 0){
                    yield return CoroutineCommandQueue.Dequeue();
                }
                else{
                    yield return null;
                }
            }
        }
    }
}