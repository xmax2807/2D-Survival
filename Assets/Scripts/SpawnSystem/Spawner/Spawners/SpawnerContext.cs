using System;
using System.Collections.Generic;
using Project.Pooling;

namespace Project.SpawnSystem
{
    /// <summary>
    /// This interface will have a permission to modify the list of spawn objects in context
    /// </summary>
    public interface ISpawnListModifier{
        void ReplaceList(ref List<uint> listIds, ISpawnContext context);
    }
    public interface IPositionSpawnModifier{
        void ReplaceSpawnPosition(ref UnityEngine.Vector3 position, SpawnPosition spawnPositionType, ISpawnContext context);
    }
    public interface ISpawnContext{
        uint SpawnCount {get;}
        UnityEngine.Vector3 CenterPosition {get;}
        void Accept(ISpawnListModifier modifier);
        void Accept(IPositionSpawnModifier modifier);
        IReadOnlyList<uint> ReadyEntitySpawnIds { get; }
        IReadOnlyList<uint> AvailableSpawnIds { get; }
        IReadOnlyList<SpawnRate> SpawnRates { get; }
    }

    public interface IBuildableContext{
        void SetCount(uint count);
        void SetSpawnPositionType(SpawnPosition spawnPositionType);
        void SetAvailableSpawnIds(uint[] ids);
        void AddSpawnRates(Rate[] rates);
    }

    /// <summary>
    /// This guy will hold some data and provide access to spawn logics
    /// </summary>
    public class SpawnerContext : ISpawnContext, IBuildableContext, IDisposable
    {
        private List<uint> spawnIds;// This will be used for other logics: Spawn alignment, rarity calculator,...
        
        uint[] availableSpawnIds;
        public IReadOnlyList<uint> AvailableSpawnIds => availableSpawnIds;

        public IReadOnlyList<SpawnRate> SpawnRates {get;private set;}

        public uint SpawnCount {get;private set;}

        private SpawnPosition spawnPositionType;
        private UnityEngine.Vector3 centerPosition;
        public UnityEngine.Vector3 CenterPosition => centerPosition;

        public IReadOnlyList<uint> ReadyEntitySpawnIds => spawnIds;


        public void Accept(ISpawnListModifier modifier)
        {
            spawnIds ??= QuickListPool<uint>.GetList();
            modifier.ReplaceList(ref spawnIds, context: this);
        }

        public void Accept(IPositionSpawnModifier modifier)
        {
            modifier.ReplaceSpawnPosition(ref centerPosition,this.spawnPositionType, context: this);
        }

        public void Dispose()
        {
            QuickListPool<uint>.ReturnList(spawnIds);
        }

        #region BuildableContext parts
        public void SetCount(uint count)
        {
            this.SpawnCount = count;
        }

        public void SetAvailableSpawnIds(uint[] ids)
        {
            this.availableSpawnIds = ids;
        }

        public void AddSpawnRates(Rate[] rates){
            SpawnRate[] array = new SpawnRate[rates.Length];
            for (int i = 0; i < rates.Length; ++i){
                array[i] = new SpawnRate(rates[i], availableSpawnIds[i]);
            }
            this.SpawnRates = array;
        }

        public void SetSpawnPositionType(SpawnPosition spawnPositionType)
        {
            this.spawnPositionType = spawnPositionType;
        }
        #endregion BuildableContext
    }
}