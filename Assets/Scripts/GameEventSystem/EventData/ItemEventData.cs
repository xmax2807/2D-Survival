using System.Collections.Generic;
using Project.Pooling;

namespace Project.GameEventSystem
{
    public struct InventoryItemEventData
    {
        public int ItemId;
        public int Count;
        public int OwnerId;

        public InventoryItemEventData(int itemId, int count, int ownerId){
            ItemId = itemId;
            Count = count;
            OwnerId = ownerId;
        }
    }

    public struct ValueRewardEventData<T>{
        public int TargetId;
        public T Amount;

        public ValueRewardEventData(int targetId, T amount){
            TargetId = targetId;
            Amount = amount;
        }
    }

    public struct ItemReward{
        public int ItemId;
        public int Count;

        public ItemReward(int itemId, int count){
            ItemId = itemId;
            Count = count;
        }
    }
    public class ItemRewardEventData{
        public int[] ItemIds {get;private set;}
        public int TargetId {get;private set;}

        private ItemRewardEventData(){}

        public static ItemRewardEventData GetDataFromPool(in int[] itemReward, in int targetId){
            var pool = m_staticPool;
            ItemRewardEventData result = pool.Get();
            result.ItemIds = itemReward;
            result.TargetId = targetId;

            return result;
        }

        public static void ReturnToPool(ItemRewardEventData data){
            m_staticPool.Return(data);
        }

        private static readonly IPool<ItemRewardEventData> m_staticPool = new CustomCreationPool<ItemRewardEventData>(() => new ItemRewardEventData());
    }
}