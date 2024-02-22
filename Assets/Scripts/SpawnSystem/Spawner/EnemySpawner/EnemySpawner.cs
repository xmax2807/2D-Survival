using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Enemy;
using Project.Pooling;
using UnityEngine;

namespace Project.SpawnSystem
{
    /// <summary>
    /// This class is bridge between spawner and enemy communication
    /// Handles 2 things: listen to death event from enemy, communicate with drop system to drop rewards
    /// </summary>
    public class EnemySpawner : ISpawner, IBuildableSpawner, Enemy.IEnemyDeathObserver
    {
        private IStorageRepository<Enemy.EnemyCore> enemyStorageRepository;
        private Dictionary<int, (uint StorageId, EnemyCore Entity)> m_activeEnemies;
        private Dictionary<int, (uint StorageId, EnemyCore Entity)> activeEnemies{
            get{
                m_activeEnemies ??= new Dictionary<int, (uint, EnemyCore)>();
                return m_activeEnemies;
            }
        }

        public ISpawnContext Context => this.m_spawnContext;

        public int ActiveCount => activeEnemies.Count;

        private uint[] m_entityIds;
        private List<ISpawnLogic> m_logicsForPrepare;
        private List<ISpawnLogic> m_logicsForSpawn;
        private IDropObservable m_dropObservable;
        EnemySpawnerContext m_spawnContext;
        private EnemyCore[] m_prepareEnemies;

        public void OnDead(EnemyDeathData data)
        {
            //TODO: call drop manager to drop rewards
            (uint StorageId, EnemyCore Entity) = activeEnemies[data.Id];

            m_dropObservable?.OnDrop(m_spawnContext.GetDropData(StorageId), data.Killer_Id);

            enemyStorageRepository.Add(StorageId, Entity);

            activeEnemies.Remove(data.Id);
        }

        public IEnumerator Prepare()
        {
            //TODO while getting enemy from pool, listen death event from enemy

            List<Task> tasks = QuickListPool<Task>.GetList();

            if(m_logicsForPrepare != null){
                foreach (var logic in m_logicsForPrepare){
                    tasks.Add(logic.PerformLogic(m_spawnContext));
                }

                yield return Task.WhenAll(tasks).AsTaskYield();
                tasks.Clear();
            }

            int count = (int)m_spawnContext.SpawnCount;
            m_prepareEnemies ??= new EnemyCore[count];
            for (int i = 0; i < count; ++i)
            {
                uint id = m_spawnContext.ReadyEntitySpawnIds[i];

                Task<EnemyCore> task = enemyStorageRepository.GetByIdAsync(id);
                tasks.Add(task);

                EnemyCore result = task.Result;
                if (result == null){
                    Debug.LogError($"[EnemySpawner] Failed to get enemy from storage. Id: {id}");
                    continue;
                }

                m_prepareEnemies[i] = result;
                activeEnemies[result.GetId()] = (id, result);
                result.GetCoreComponent<IEnemyStateObservable>()?.AddDeathEventObserver(this);
            }

            yield return Task.WhenAll(tasks).AsTaskYield();
            QuickListPool<Task>.ReturnList(tasks);
        }

        public IEnumerator Spawn()
        {
            if(m_logicsForSpawn != null){
                foreach (var logic in m_logicsForSpawn){
                    yield return logic.PerformLogic(m_spawnContext).AsTaskYield();
                }
            }
            for(int i = m_prepareEnemies.Length - 1; i >= 0; --i){
                var Entity = m_prepareEnemies[i];
                Entity.transform.position = m_spawnContext.CenterPosition;
                Entity.gameObject.SetActive(true);
            }
        }


        #region Buildable parts
        public void SetContext(ISpawnContext context){
            m_spawnContext = new EnemySpawnerContext(context);
        }
        public void AddSpawnEntities(uint[] ids)
        {
            m_entityIds = ids;
        }
        public void GetStorageRepositoryFrom(IStorageGetter storageGetter)
        {
            bool canGet = storageGetter.TryGetStorageRepository<EnemyCore>(out enemyStorageRepository);
            if(!canGet) throw new System.Exception("Failed to get storage repository from spawner");
        }

        public void AddDropObservable(IDropObservable dropObservable)
        {
            this.m_dropObservable = dropObservable;
        }
        public void AddDropFor(uint id, DropData dropData)
        {
            m_spawnContext.AddDropForEntity(id, dropData);
        }
        public ISpawner CastToSpawner() => this;

        public void AddSpawnLogicForPrepare(ISpawnLogic logic)
        {
            if(logic ==null) return;
            m_logicsForPrepare??= new List<ISpawnLogic>();
            m_logicsForPrepare.Add(logic);
        }

        public void AddSpawnLogicForSpawn(ISpawnLogic logic)
        {
            if(logic ==null) return;
            m_logicsForSpawn ??= new List<ISpawnLogic>();
            m_logicsForSpawn.Add(logic);
        }
        #endregion
    }
}