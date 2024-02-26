using Project.Pooling;
using UnityEngine;

namespace Project.LootSystem
{
    public class AutoLootableItemPreset
    {
        public readonly AutoLootableItem itemPrefab;
        public readonly FeedbackData[] lootFeedbacks;
        public AutoLootableItemPreset(AutoLootableItem itemPrefab, FeedbackData[] lootFeedbacks){
            this.itemPrefab = itemPrefab;
            this.lootFeedbacks = lootFeedbacks;
        }
    }
    public enum AutoLootableItemType : byte {Gold,Exp}

    public abstract class LootTableSource : ScriptableObject
    {
        protected abstract AutoLootableItemPreset GoldAutoLootableItemPreset {get;}
        protected abstract AutoLootableItem GetGoldObject(int amount);
        protected abstract void ReturnGoldObject(AutoLootableItem gold);

        public virtual void Initialize(Transform container) { }

        public AutoLootableItem GetAutoLootableItem(AutoLootableItemType type, int amount)
        {
            return type switch
            {
                AutoLootableItemType.Gold => GetGoldObject(amount),
                AutoLootableItemType.Exp => null,
                _ => null,
            };
        }

        public void ReturnAutoLootableItem(AutoLootableItemType type, AutoLootableItem item)
        {
            switch (type)
            {
                case AutoLootableItemType.Gold: ReturnGoldObject(item); break;
                case AutoLootableItemType.Exp: break;
                default: break;
            }
        }

        public FeedbackData[] GetAutoLootableItemFeedbacks(AutoLootableItemType type)
        {
            return type switch
            {
                AutoLootableItemType.Gold => GoldAutoLootableItemPreset.lootFeedbacks,
                AutoLootableItemType.Exp => null,
                _ => null,
            };
        }
    }
}