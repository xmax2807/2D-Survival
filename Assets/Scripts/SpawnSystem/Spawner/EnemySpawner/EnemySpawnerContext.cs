using System.Collections.Generic;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class EnemySpawnerContext : ISpawnContext
    {
        private ISpawnContext wrappedContext;
        private Dictionary<uint, DropData> dropDataMap;

        public uint SpawnCount => wrappedContext.SpawnCount;

        public IReadOnlyList<uint> ReadyEntitySpawnIds => wrappedContext.ReadyEntitySpawnIds;

        public IReadOnlyList<uint> AvailableSpawnIds => wrappedContext.AvailableSpawnIds;

        public IReadOnlyList<SpawnRate> SpawnRates => wrappedContext.SpawnRates;

        public Vector3 CenterPosition => wrappedContext.CenterPosition;

        public EnemySpawnerContext(ISpawnContext context)
        {
            this.wrappedContext = context;
        }

        public DropData GetDropData(uint entityId) => dropDataMap[entityId];
        public void AddDropForEntity(uint entityId, DropData dropData)
        {
            dropDataMap ??= new Dictionary<uint, DropData>();
            dropDataMap[entityId] = dropData;
        }

        public void Accept(ISpawnListModifier modifier)
        {
            wrappedContext.Accept(modifier);
        }

        public void Accept(IPositionSpawnModifier modifier)
        {
            wrappedContext.Accept(modifier);
        }
    }
}