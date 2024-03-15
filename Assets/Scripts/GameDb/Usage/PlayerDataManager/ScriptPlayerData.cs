using System;
using UnityEngine;

namespace Project.GameDb
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "GameDb/PlayerDataContainer", order = 0)]
    public class ScriptPlayerData : ScriptableObject, IPlayerHUDRepository, IPlayerRepository
    {
        public event Action<int> PlayerHealthChangedEvent;
        public event Action<int> PlayerReceiveGoldEvent;
        public event Action<int> PlayerMaxHealthChangedEvent;

        [SerializeField] Project.PlayerBehaviour.PlayerData PlayerData;
        private Project.PlayerBehaviour.PlayerInventoryData _inventoryData;

        public int Health
        {
            get => (int)PlayerData.Health;
            set
            {
                PlayerData.Health = (uint)value;
                PlayerHealthChangedEvent?.Invoke(value);
            }
        }
        public int Gold
        {
            get => (int)_inventoryData.Gold;
            set
            {
                int old = _inventoryData.Gold;
                _inventoryData.Gold = value;
                PlayerReceiveGoldEvent?.Invoke(_inventoryData.Gold - old);
            }
        }

        public int MaxHealth { 
            get => (int)PlayerData.MaxHealth; 
            set {
                PlayerData.MaxHealth = (uint)value;
                PlayerMaxHealthChangedEvent?.Invoke(value);
            } 
        }

        public void MapFrom(Project.PlayerBehaviour.PlayerData playerData)
        {
            PlayerData = playerData;
            PlayerMaxHealthChangedEvent?.Invoke((int)playerData.MaxHealth);
            PlayerHealthChangedEvent?.Invoke((int)playerData.Health);
        }

        public void MapFrom(PlayerBehaviour.PlayerInventoryData inventoryData)
        {
            _inventoryData = inventoryData;
            PlayerReceiveGoldEvent?.Invoke(inventoryData.Gold);
        }
    }
}