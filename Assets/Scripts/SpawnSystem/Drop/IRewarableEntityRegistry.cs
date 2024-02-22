namespace Project.SpawnSystem
{
    public interface IRewardableEntity
    {
        void GiveExp(uint amount);
        void GiveItems(params uint[] ids);

        float GetDropBonus();
        float GetExpBonus();
    }

    public interface IRewarableEntityRegistry
    {
        void Register(int id, IRewardableEntity entity);
        void Unregister(int id);

        IRewardableEntity GetById(int id);
    }
}