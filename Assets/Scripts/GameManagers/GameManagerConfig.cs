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
using Project.VisualEffectSystem;
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
        
        void GetParams()
        {
            GameParams.ScriptableParamProvider paramProvider = Resources.Load<GameParams.ScriptableParamProvider>("ParamProvider");
            paramProvider.Initialize();
            _params = paramProvider;
        }
    }
}