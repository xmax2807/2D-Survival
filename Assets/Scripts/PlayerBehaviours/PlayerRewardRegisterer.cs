using Project.CharacterBehaviour;
using Project.SpawnSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    public class PlayerRewardRegisterer : MonoCoreComponent, IRewardableEntity
    {
        [SerializeField] private ScriptableRewardableEntityRegistry _registry;
        private PlayerDataController _playerData;
        PlayerDataController playerDataController{
            get{
                if(_playerData == null) {
                    _playerData = Core.GetCoreComponent<PlayerDataController>();
                }
                return _playerData;
            }
        }
        public int Id => Core.GetId();

        protected override void AfterAwake()
        {
            _registry.Register(Id, this);
            AddCoreComponent<PlayerRewardRegisterer>(this);
        }
        public float GetDropBonus()
        {
            return 0;
        }

        public float GetExpBonus()
        {
            return 1;
        }
        public float GetGoldBonus()
        {
            return 1;
        }

        public void GiveExp(int amount)
        {
            //TODO
        }

        public void GiveItems(params int[] ids)
        {
            //TODO
        }

        public void GiveGold(int amount)
        {
            if(playerDataController == null) {
                #if UNITY_EDITOR
                Debug.LogError("PlayerDataController is not created");
                #endif
                return;
            }
            playerDataController.ReceiveGold(amount);
        }
    }
}