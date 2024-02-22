namespace Project.SpawnSystem
{
    public interface IExpDrop
    {
        void StartDrop(uint amount, IRewardableEntity target);
    }

    public class InstantApplyExpDrop : IExpDrop
    {

        public void StartDrop(uint amount, IRewardableEntity target)
        {
            target.GiveExp((uint)(amount * target.GetExpBonus()));
        }
    }
}