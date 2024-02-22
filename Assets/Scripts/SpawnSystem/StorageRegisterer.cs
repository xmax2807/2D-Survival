using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.SpawnSystem
{
    public interface IStorageRegisterer{
        void RegisterTo(IStorageRegistry registry);
        void UnregisterFrom(IStorageRegistry registry);
    }

    public interface IStorageGetter{
        IStorageRepository<TEntity> GetStorageRepository<TEntity>();
        bool TryGetStorageRepository<TEntity>(out IStorageRepository<TEntity> repository);
    }

    public interface IStorageRegistry{
        void AddStorage<TEntity>(IStorageRepository<TEntity> repository);
        void RemoveStorage<TEntity>(IStorageRepository<TEntity> repository);
    }

    public abstract class ScriptableGenericStorage : ScriptableObject, IStorageRegisterer
    {
        public abstract void RegisterTo(IStorageRegistry registry);

        public abstract void UnregisterFrom(IStorageRegistry registry);
    }

    public abstract class ScriptableGenericStorage<T> : ScriptableGenericStorage{
        protected abstract IStorageRepository<T> storageRepository {get;} 

        public override void RegisterTo(IStorageRegistry registry){
            registry.AddStorage<T>(storageRepository);
        }

        public override void UnregisterFrom(IStorageRegistry registry){
            registry.RemoveStorage<T>(storageRepository);
        }
    }
}