using System;

namespace Project.SpawnSystem
{
    /// <summary>
    /// This encapsulates data about drop from SpawnerData
    /// </summary>
    public class DropData{
        public readonly DropItem[] items;
        public readonly int ExpAmount;
        public readonly int GoldAmount;

        public DropData(DropItem[] items, int expAmount, int goldAmount){
            if(items == null){
                this.items = Array.Empty<DropItem>();
            }
            else{
                this.items = new DropItem[items.Length];
                Array.Copy(items, this.items, items.Length);
            }
            ExpAmount = expAmount;
            GoldAmount = goldAmount;
        }

        public int[] GetItemIds(){
            int[] result = new int[items.Length];
            for(int i = 0; i < items.Length; ++i){
                result[i] = items[i].ItemId;
            }
            return result;
        }
    }

    [System.Serializable]
    public struct DropItem{
        public Rate rate;
        public ushort ItemId;
    }
}