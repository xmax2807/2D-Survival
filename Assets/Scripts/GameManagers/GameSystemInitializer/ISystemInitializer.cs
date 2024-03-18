using System.Collections;
using UnityEngine;

namespace Project.Manager
{
    public interface ISystemInitializer
    {
        bool IsInitialized { get; }
        IEnumerator InitializeTask {get;}
        IEnumerator Initialize(GameManager manager);
    }

    public abstract class MonoSystemInitializer : UnityEngine.MonoBehaviour, ISystemInitializer
    {
        
        [Header("Dependencies"), SerializeField] MonoSystemInitializer[] _dependencies;
        public bool IsInitialized { get; protected set; }
        public IEnumerator InitializeTask {get; protected set;}

        public IEnumerator Initialize(GameManager manager){
            if(IsInitialized) yield break;

            //wait for _dependencies are initialized
            for(int i = 0; i < _dependencies.Length; ++i){
                MonoSystemInitializer dependency = _dependencies[i];

                if(dependency == null || dependency.IsInitialized) continue;
                InitializeTask = dependency.InitializeTask;

                yield return InitializeTask;
            }
            
            InitializeTask = InitializeInternal(manager);

            yield return InitializeTask;
            
            InitializeTask = null;
            IsInitialized = true;
        }

        protected abstract IEnumerator InitializeInternal(GameManager manager);
    }
}