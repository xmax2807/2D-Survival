using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.SpawnSystem
{
    public class RarityCalculator : ISpawnLogic, ISpawnListModifier
    {
        public Task<bool> PerformLogic(ISpawnContext context)
        {
            context.Accept(this);
            return Task.FromResult(true);
        }

        public void ReplaceList(ref List<uint> listIds, ISpawnContext context)
        {
            if (listIds == null)
            {
                listIds = new List<uint>((int)context.SpawnCount);
            }
            var spawnRates = context.SpawnRates;
            int count = listIds.Count;
            while (count < context.SpawnCount)
            {
                listIds.Add(0);
                ++count;
            }

            for (int i = 0; i < context.SpawnCount; ++i)
            {
                //TODO calculate rarity
                listIds[i] = CalculateRarity(ref spawnRates);
            }
        }

        /// <summary>
        /// Assume the array is sorted from higher to lower
        /// </summary>
        /// <param name="spawnRates"></param>
        /// <returns></returns>
        private uint CalculateRarity(ref IReadOnlyList<SpawnRate> spawnRates)
        {
            Rate random = UnityEngine.Random.Range(0, 100);
            for (int i = spawnRates.Count - 1; i >= 0; --i)
            {
                if(random < spawnRates[i].Rate)
                {
                    return spawnRates[i].EntityID;
                }
            }
            return spawnRates[spawnRates.Count - 1].EntityID;
        }

    }

    
}