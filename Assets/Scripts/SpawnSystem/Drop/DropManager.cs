namespace Project.SpawnSystem
{
    public interface IDropHandler{
        void DropItems(DropItem[] data, int targetId);
        void DropExp(uint amount, int targetId);
    }
    internal class DropManager : IDropHandler
    {
        private IExpDrop m_expDrop;
        private IItemDropLogic m_itemDropLogic;
        private IRewarableEntityRegistry m_dropRegistry;

        public DropManager(IExpDrop expDrop, IItemDropLogic itemDrop, IRewarableEntityRegistry dropRegistry){
            m_expDrop = expDrop;
            m_itemDropLogic = itemDrop;
            m_dropRegistry = dropRegistry;

            if(m_expDrop == null || m_dropRegistry == null){
                throw new System.NullReferenceException("Can't initialize DropManager while there is a null reference");
            }
        }

        public void DropExp(uint amount, int targetId)
        {
            IRewardableEntity target = m_dropRegistry.GetById(targetId);
            if(target == null) return;

            m_expDrop.StartDrop(amount, target);
        }

        public void DropItems(DropItem[] data, int targetId)
        {
            IRewardableEntity target = m_dropRegistry.GetById(targetId);
            if(target == null) return;

            m_itemDropLogic.Drop(data, target);
        }
    }
}