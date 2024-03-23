using CommunityToolkit.Mvvm.Messaging;
using Cysharp.Threading.Tasks;
using MVVMToolkit;
using MVVMToolkit.DependencyInjection;
using Project.GameDb;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] PlayerManager playerManager;
        [SerializeField] UIRoot PlayerHUD;
        public void Start() => InitializeAsync().Forget();

        async UniTask InitializeAsync()
        {
            // We call UIRoot.Initialize method and provide StrongReferenceMessenger and ServiceProvider instances.
            // If you have external services on which your Views or ViewModels rely you must register them
            // before calling Initialize.
            var messenger = new StrongReferenceMessenger();
            var serviceProvider = new ServiceProvider();
            serviceProvider.RegisterService<IPlayerHUDRepository>(playerManager.ScriptPlayerData);
            // Before we can make any calls to UI, we need to await it's initialization
            await PlayerHUD.Initialize(messenger, serviceProvider);
            
            playerManager.ScriptPlayerData.MapFrom(playerManager.PlayerDataBinding.PlayerData);
            playerManager.ScriptPlayerData.MapFrom(playerManager.PlayerInventoryDataBinding.PlayerInventoryData);
        }
    }
}