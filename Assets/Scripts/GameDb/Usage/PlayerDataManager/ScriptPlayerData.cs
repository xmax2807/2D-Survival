using System;
using UnityEngine;

namespace Project.GameDb
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "GameDb/PlayerDataContainer", order = 0)]
    public class ScriptPlayerData : ScriptableObject, IPlayerHUDRepository, IPlayerRepository
    {
        public event Action<int> PlayerHealthChangedEvent;
        public event Action<int> PlayerReceiveGoldEvent;
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

        public void MapFrom(Project.PlayerBehaviour.PlayerData playerData)
        {
            PlayerData = playerData;
        }

        public void MapFrom(PlayerBehaviour.PlayerInventoryData inventoryData)
        {
            _inventoryData = inventoryData;
        }
    }
}