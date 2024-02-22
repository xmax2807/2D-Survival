using System;

namespace Project.SpawnSystem
{
    /// <summary>
    /// This encapsulates data about drop from SpawnerData
    /// </summary>
    public class DropData{
        public readonly DropItem[] items;
        public readonly uint ExpAmount;

        public DropData(DropItem[] items, uint expAmount){
            if(items == null){
                this.items = Array.Empty<DropItem>();
            }
            else{
                this.items = new DropItem[items.Length];
                Array.Copy(items, this.items, items.Length);
            }
            ExpAmount = expAmount;
        }
    }

    [System.Serializable]
    public struct DropItem{
        public Rate rate;
        public ushort ItemId;
    }
}