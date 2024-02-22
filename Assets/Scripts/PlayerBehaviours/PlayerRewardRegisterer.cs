using Project.CharacterBehaviour;
using Project.SpawnSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    public class PlayerRewardRegisterer : MonoCoreComponent, IRewardableEntity
    {
        [SerializeField] private ScriptableRewardableEntityRegistry _registry;
        protected override void AfterAwake(){
            _registry.Register(Core.GetId(), this);
        }
        public float GetDropBonus()
        {
            return 0;
        }

        public float GetExpBonus()
        {
            return 0;
        }

        public void GiveExp(uint amount)
        {
            //TODO
        }

        public void GiveItems(params uint[] ids)
        {
            //TODO
        }
    }
}