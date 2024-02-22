using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.SpawnSystem
{
    [System.Serializable]
    public class StorageEntityWrapper<TEntity> where TEntity : UnityEngine.Object{
        [UnityEngine.SerializeField] private uint _id;
        [UnityEngine.SerializeField] private TEntity _entity;
        public uint Id => _id;
        public TEntity Entity => _entity;
    }
    public interface IStorageRepository<T>{
        T GetById(uint id);
        Task<T> GetByIdAsync(uint id);
        void Add(uint id, T entity);
    }

    public class DictionaryStorageRepository<T> : IStorageRepository<T>{
        private readonly Dictionary<uint, T> _repository = new Dictionary<uint, T>();

        public void Add(uint id, T entity)
        {
            _repository.Add(id, entity);
        }

        public T GetById(uint id)
        {
            if(!_repository.ContainsKey(id)){
                return default;
            }
            return _repository[id];
        }

        public Task<T> GetByIdAsync(uint id)
        {
            return Task.FromResult(_repository[id]);
        }
    }
}