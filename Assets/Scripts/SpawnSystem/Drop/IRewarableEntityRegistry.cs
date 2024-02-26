namespace Project.SpawnSystem
{
    public interface IRewardableEntity
    {
        int Id { get; }
        void GiveGold(int amount);
        void GiveExp(int amount);
        void GiveItems(params int[] ids);

        float GetDropBonus();
        float GetExpBonus();
        float GetGoldBonus();
    }

    public interface IRewarableEntityRegistry
    {
        void Register(int id, IRewardableEntity entity);
        void Unregister(int id);

        IRewardableEntity GetById(int id);
    }
}