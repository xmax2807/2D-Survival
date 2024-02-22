using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "DropManager", menuName = "SpawnSystem/ScriptableDropManager", order = 1)]
    public class ScriptableDropManager : ScriptableObject, IDropHandler
    {
        public enum DropLogicType{
            Instant,
            Spawn
        }

        [SerializeField] private DropLogicType _expDropLogicType;
        [SerializeField] private DropLogicType _itemDropLogicType;
        [SerializeField] private ScriptableRewardableEntityRegistry _rewardableEntityRegistry;
        private IDropHandler _wrappeeHandler;
        public IDropObservable DropObservable {get;private set;}
        public bool IsInitialized {get;private set;}
        /// <summary>
        /// Build the manager
        /// </summary>
        public void OnEnable(){
            _wrappeeHandler = new DropManager(InitDropLogic(), InitItemDropLogic(), _rewardableEntityRegistry);
            DropObservable = new DefaultDropObservable(_wrappeeHandler);
        }

        private IExpDrop InitDropLogic(){
            switch(_expDropLogicType){
                case DropLogicType.Instant:
                    return new InstantApplyExpDrop();
                case DropLogicType.Spawn:
                    //TODO: SpawnExp
                    break;
            }

            return new InstantApplyExpDrop();
        }

        private IItemDropLogic InitItemDropLogic(){
            switch(_itemDropLogicType){
                case DropLogicType.Instant:
                    return new InstantItemDropLogic();
                case DropLogicType.Spawn:
                    //TODO: Spawn
                    break;
            }
            return new InstantItemDropLogic();
        }

        public void DropItems(DropItem[] data, int targetId) => _wrappeeHandler.DropItems(data, targetId);
        public void DropExp(uint amount, int targetId) => _wrappeeHandler.DropExp(amount, targetId);
    }
}