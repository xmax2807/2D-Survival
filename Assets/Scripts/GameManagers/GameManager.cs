using System;
using System.Collections;
using System.Collections.Generic;
using Project.GameEvent;
using Project.GameStateCommand;
using UnityEngine;

namespace Project.Manager
{
    public partial class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                // game is not playing return null
                if(!Application.isPlaying) return null;

                if (_instance == null)
                {
                    AutoCreate();
                }
                return _instance;
            }
        }

        private static void AutoCreate()
        {
            new GameObject("GameManager").AddComponent<GameManager>();
        }

        #region Service Locator
        readonly Dictionary<Type, object> m_serviceMap = new();
        internal void AddService<TInterface, TInstance>(TInstance instance) where TInstance : TInterface
        {
            Type interfaceType = typeof(TInterface);
            if(m_serviceMap.ContainsKey(interfaceType))
            {
                m_serviceMap[interfaceType] = instance;
            }
            else
            {
                m_serviceMap.Add(interfaceType, instance);
            }
        }

        internal void AddService<TService>(TService service){
            this.AddService<TService, TService>(service);
        }
        public static bool TryGetService<TInterface>(out TInterface service){
            if(Instance == null || Instance.m_serviceMap == null){
                service = default;
                return false;
            }
            TInterface result = Instance.GetService<TInterface>();
            service = result;
            return result != null;
        }
        public TInterface GetService<TInterface>(){
            Type interfaceType = typeof(TInterface);
            if(m_serviceMap.ContainsKey(interfaceType))
            {
                return (TInterface)m_serviceMap[interfaceType];
            }
            return default;
        }
        #endregion


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            InitializeCoroutine();
            InitializeGameCommands();
        }

        private void InitializeGameCommands()
        {
            var commandProvider = Resources.Load<StateCommandProvider>("StateCommandProvider");
            commandProvider.AddCommand(new LoadSystemCommand());
            commandProvider.AddCommand(new TestCommand());
            commandProvider.AddCommand(new LoadMainGamePlayScene("SampleScene"));
        }

        private void InitializeCoroutine()
        {
            Utils.Coroutines.Initialize(this);
            
            CoroutineCommandQueue = new Queue<IEnumerator>();
            StartCoroutine(RunQueueCommands());
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

        public void LoadEssentialSystems(){
            MonoSystemInitializer[] systems = GetComponentsInChildren<MonoSystemInitializer>();

            for(int i = 0; i < systems.Length; ++i){
                if(systems[i].IsInitialized) continue;
                StartCoroutine(systems[i].Initialize(this));
            }
        }

        public IEnumerator EssentialSystemsAwaiter(){
            MonoSystemInitializer[] systems = GetComponentsInChildren<MonoSystemInitializer>();
            IEnumerator awaiter = null;
            for(int i = 0; i < systems.Length; ++i){
                if(systems[i].IsInitialized) continue;

                awaiter = Combine(first: awaiter, then: AsIEnumerator(StartCoroutine(systems[i].Initialize(this))));
            }

            return awaiter;
        }

        private static IEnumerator Combine(IEnumerator first, IEnumerator then)
        {
            if(first != null){
                yield return first;
            }
            if(then != null){
                yield return then;
            }
        }

        private IEnumerator AsIEnumerator(Coroutine coroutine)
        {
            yield return coroutine;
        }
        
        private void OnApplicationQuit()
        {
            if (_instance == this)
            {
                Destroy(_instance.m_visibleRendererStorage);
                _instance = null;

                Debug.Log("Quit Game");
            }
        }
    }
}
