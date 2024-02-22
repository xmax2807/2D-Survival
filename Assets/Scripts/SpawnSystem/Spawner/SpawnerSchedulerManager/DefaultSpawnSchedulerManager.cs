using System.Threading.Tasks;
using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "DefaultSpawnSchedulerManager", menuName = "SpawnSystem/DefaultSpawnSchedulerManager")]
    public class DefaultSpawnSchedulerManager : ScriptableObject, ISpawnSchedulerManager
    {
        [SerializeField] private SpawnConfiguration[] spawnConfigurations;
        [SerializeField] private ScriptableBuilderDirector m_scriptableBuilderDirector;
        [SerializeField] private StorageDatabase m_storageDatabase;
        private ISpawnScheduler[] m_spawnSchedulers;
        public Task<ISpawnScheduler[]> GetAllSpawnSchedulerAsync()
        {
            if(m_spawnSchedulers != null) return Task.FromResult(m_spawnSchedulers);

            m_spawnSchedulers = new ISpawnScheduler[spawnConfigurations.Length];
            for(int i = 0; i < spawnConfigurations.Length; ++i)
            {
                ISpawner spawner = m_scriptableBuilderDirector.BuildSpawner(spawnConfigurations[i].SpawnType, spawnConfigurations[i].SpawnData, m_storageDatabase);
                ICommandSelector selector = m_scriptableBuilderDirector.BuildSpawnCommandSelector(spawnConfigurations[i].SpawnSchedulerData);
                m_spawnSchedulers[i] = new DefaultSpawnScheduler(selector, spawner);
            }
            return Task.FromResult(m_spawnSchedulers);
        }
    }
}