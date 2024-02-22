using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Pooling;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class StoragePool<T> : IStorageRepository<T> where T : UnityEngine.Component
    {
        private IStorageRepository<T> _repository;
        private Dictionary<uint, AutoExpandPool<T>> _pools;
        private Action<T> _OnReturnToPool;
        private Action<T> _OnGetFromPool;

        public StoragePool(IStorageRepository<T> repos, Action<T> OnReturnToPool = null, Action<T> OnGetFromPool = null)
        {
            _repository = repos;
            _pools = new Dictionary<uint, AutoExpandPool<T>>();
            _OnReturnToPool = OnReturnToPool;
            _OnGetFromPool = OnGetFromPool;
        }

        public void Add(uint id, T entity)
        {
            if(entity == null || _repository.GetById(id) == null){
                return;
            }

            if(!_pools.ContainsKey(id)){
                AddNewPool(id);
            }
            _OnReturnToPool?.Invoke(entity);
            _pools[id].Return(entity);
        }

        private void AddNewPool(uint id){
            _pools.Add(id, new CustomCreationPool<T>(createCallback: ()=>CreateObject(id)));
        }

        private T CreateObject(uint id){
            T template = _repository.GetById(id);
            var obj = GameObject.Instantiate(template);
            obj.gameObject.SetActive(false);
            return obj;
        }

        public T GetById(uint id)
        {
            if(!_pools.ContainsKey(id)){
                AddNewPool(id);
            }
            var obj = _pools[id].Get();
            _OnGetFromPool?.Invoke(obj);
            return obj;
        }

        public Task<T> GetByIdAsync(uint id)
        {
            return Task.FromResult(GetById(id));
        }
    }
}