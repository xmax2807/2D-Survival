namespace Project.SpawnSystem
{
    public class IndividualEnemySpawnerBuilder : IIndividualSpawnerBuilder
    {
        private SpawnLogicProvider m_spawnLogicProvider;
        private IBuildableSpawner m_buildableSpawner;
        public IBuildableSpawner Result => m_buildableSpawner;

        private IBuildableSpawner Init(){
            m_buildableSpawner = new EnemySpawner();
            return m_buildableSpawner;
        }

        public IndividualEnemySpawnerBuilder(SpawnLogicProvider spawnLogicProvider){
            m_buildableSpawner = Init();
            m_spawnLogicProvider = spawnLogicProvider;
        }

        public IIndividualSpawnerBuilder AddDropObservable(IDropObservable dropObservable)
        {
            m_buildableSpawner.AddDropObservable(dropObservable);
            return this;
        }

        public IIndividualSpawnerBuilder AddSpawnEntities(IndividualData[] entityDatas, IStorageGetter storageGetter)
        {
            uint[] entityIds = new uint[entityDatas.Length];
            for(int i = 0; i < entityDatas.Length; ++i){
                entityIds[i] = entityDatas[i].EntityID;
                m_buildableSpawner.AddDropFor(entityDatas[i].EntityID, new DropData(entityDatas[i].DropItems, entityDatas[i].ExpAmount));
            }
            m_buildableSpawner.AddSpawnEntities(entityIds);
            m_buildableSpawner.GetStorageRepositoryFrom(storageGetter);

            if(m_spawnLogicProvider != null){
                m_buildableSpawner.AddSpawnLogicForPrepare(m_spawnLogicProvider.GetSpawnLogic(SpawnLogicProvider.SpawnLogicType.RateCalculator));
                m_buildableSpawner.AddSpawnLogicForSpawn(m_spawnLogicProvider.GetSpawnLogic(SpawnLogicProvider.SpawnLogicType.SpawnPosition));
            }
            return this;
        }

        public IIndividualSpawnerBuilder Reset()
        {
            m_buildableSpawner = Init();
            return this;
        }
    }
}