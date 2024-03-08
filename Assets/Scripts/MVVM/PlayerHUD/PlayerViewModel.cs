using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVMToolkit;
using MVVMToolkit.DependencyInjection;
using Project.GameDb;
namespace Project.MVVM.PlayerHUD
{
    public partial class PlayerViewModel : ViewModel
    {
        [Inject]
        private IPlayerHUDRepository playerHUDRepository;

        [ObservableProperty] int _health;
        int _coin;
        readonly CoinReceiveData _coinReceiveData = new();

        protected override void OnInit(){
            playerHUDRepository.PlayerHealthChangedEvent += OnPlayerHealthChanged;
            playerHUDRepository.PlayerReceiveGoldEvent += OnPlayerReceiveCoin;
        }

        private void OnPlayerReceiveCoin(int additionalValue)
        {
            _coinReceiveData.oldCoinValue = _coin;
            _coinReceiveData.newCoinValue = _coin += additionalValue;
            this.Messenger.Send<CoinReceiveData>(_coinReceiveData);
        }

        protected override void OnDestroy(){
            playerHUDRepository.PlayerHealthChangedEvent -= OnPlayerHealthChanged;
            playerHUDRepository.PlayerReceiveGoldEvent -= OnPlayerReceiveCoin;
        }

        private void OnPlayerHealthChanged(int value)
        {
            Health = value;
        }
    }
}