namespace Project.Enemy
{
    public interface IEnemyDeathObserver
    {
        void OnDead(EnemyDeathData data);
    }
}