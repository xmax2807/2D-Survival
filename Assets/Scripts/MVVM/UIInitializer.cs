using CommunityToolkit.Mvvm.Messaging;
using Cysharp.Threading.Tasks;
using MVVMToolkit;
using MVVMToolkit.DependencyInjection;
using Project.GameDb;
using UnityEngine;
using UnityEngine.Playables;

namespace Project.MVVM
{
    public class UIInitializer : MonoBehaviour
    {
        [SerializeField] PlayableDirector playableDirector;
        [SerializeField] ScriptPlayerData playerData;
        // Internally UI is initialized in Awake
        // Actual initialization should be done at least after Start 
        void Start() => InitializeAsync().Forget();

        private async UniTask InitializeAsync()
        {
            var root = GetComponent<UIRoot>();

            // We call UIRoot.Initialize method and provide StrongReferenceMessenger and ServiceProvider instances.
            // If you have external services on which your Views or ViewModels rely you must register them
            // before calling Initialize.
            var messenger = new StrongReferenceMessenger();
            var serviceProvider = new ServiceProvider();
            serviceProvider.RegisterService<IPlayerHUDRepository>(playerData);

            
            // Before we can make any calls to UI, we need to await it's initialization
            await root.Initialize(messenger, serviceProvider);

            playerData.MapFrom(new PlayerBehaviour.PlayerData(1000, 1, 10, 5, 10));
            playerData.MapFrom(new PlayerBehaviour.PlayerInventoryData());
            playerData.Health = 100;
            //messenger.Send<OpenTestViewMessage>();
        }
    }
}