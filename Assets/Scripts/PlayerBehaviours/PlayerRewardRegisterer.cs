using Project.CharacterBehaviour;
using Project.SpawnSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    public class PlayerRewardRegisterer : MonoCoreComponent, IRewardableEntity
    {
        [SerializeField] private ScriptableRewardableEntityRegistry _registry;

        public int Id => Core.GetId();

        protected override void AfterAwake(){
            _registry.Register(Id, this);
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
        }
    }
}