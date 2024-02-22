using System.Collections.Generic;
using UnityEngine;

namespace Project.SpawnSystem
{
    [CreateAssetMenu(menuName = "SpawnSystem/StorageDatabase", fileName = "StorageDatabase")]
    public class StorageDatabase : ScriptableObject, IStorageGetter, IStorageRegistry
    {
        [SerializeField] ScriptableGenericStorage[] registeredStorages;
        private Dictionary<System.Type, object> m_storageRepositories = new Dictionary<System.Type, object>();

        void OnEnable(){
            foreach(var storage in registeredStorages){
                storage.RegisterTo(this);
            }
        }

        void OnDisable(){
            foreach(var storage in registeredStorages){
                storage.UnregisterFrom(this);
            }
        }

        public void AddStorage<TEntity>(IStorageRepository<TEntity> repository)
        {
            if(m_storageRepositories.ContainsKey(typeof(TEntity))){
                Debug.LogWarning($"Storage {typeof(TEntity)} already exists and replaced with new one");
            }
            m_storageRepositories[typeof(TEntity)] = repository;
        }

        public IStorageRepository<TEntity> GetStorageRepository<TEntity>()
        {
            bool result = m_storageRepositories.TryGetValue(typeof(TEntity), out object repository);
            return result == true ? (IStorageRepository<TEntity>)repository : null;
        }

        public void RemoveStorage<TEntity>(IStorageRepository<TEntity> repository)
        {
            if(m_storageRepositories.ContainsKey(typeof(TEntity))){
                m_storageRepositories.Remove(typeof(TEntity));
            }
        }

        public bool TryGetStorageRepository<TEntity>(out IStorageRepository<TEntity> repository)
        {
            repository = GetStorageRepository<TEntity>();
            return repository != null;
        }
    }
}