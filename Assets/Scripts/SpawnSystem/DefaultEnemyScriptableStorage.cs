using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Enemy;
using UnityEngine;
namespace Project.SpawnSystem
{
    /// <summary>
    /// This class will hold some configuration about template enemies
    /// </summary>
    
    [CreateAssetMenu(fileName = "DefaultEnemyStorage", menuName = "SpawnSystem/DefaultEnemyScriptableStorage", order = 1)]
    public class DefaultEnemyScriptableStorage : ScriptableGenericStorage<EnemyCore>
    {
        //TODO add registered EnemyCore[]
        [SerializeField] private StorageEntityWrapper<EnemyCore>[] registeredEnemies;
        private readonly IStorageRepository<EnemyCore> _repository = new DictionaryStorageRepository<EnemyCore>();
        private StoragePool<EnemyCore> _repositoryPool;

        protected override IStorageRepository<EnemyCore> storageRepository { get => _repositoryPool ??= new StoragePool<EnemyCore>(_repository, OnReturnToPool: OnReturnToPool); }

        private void OnReturnToPool(EnemyCore core)
        {
            core.gameObject.SetActive(false);
        }

        void OnEnable(){
            if(registeredEnemies != null){
                foreach(var enemy in registeredEnemies){
                    _repository.Add(enemy.Id, enemy.Entity);
                }
            }
        }
    }
}