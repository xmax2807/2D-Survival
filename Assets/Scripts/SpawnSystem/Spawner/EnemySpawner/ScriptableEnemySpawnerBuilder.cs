using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "EnemySpawnerBuilder", menuName = "SpawnSystem/EnemySpawnerBuilder")]
    public class ScriptableEnemySpawnerBuilder : ScriptableSpawnerBuilder
    {
        [SerializeField] SpawnLogicProvider _spawnLogicProvider;
        IIndividualSpawnerBuilder individualBuilder;
        public override IIndividualSpawnerBuilder GetIndividualBuilder()
        {
            individualBuilder ??= new IndividualEnemySpawnerBuilder(_spawnLogicProvider);
            return individualBuilder;
        }
    }
}