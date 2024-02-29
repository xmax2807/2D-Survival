using System;
using UnityEngine;

namespace Project.LootSystem
{
    public interface ILootHandler
    {
        event Action<IReadOnlyAutoLootContext, Transform> OnLootComplete;
        void HandleAutoLoot(IReadOnlyAutoLootContext context);
    }

    public enum OutcomeType : byte{
        Gold,
        Exp,
        Item
    }
    public readonly struct LootOutcome{
        public readonly OutcomeType OutcomeType;
        public readonly object Value;
        public readonly T GetValue<T>() {
            if(Value is T t){
                return t;
            }
            return default;
        }

        internal LootOutcome(OutcomeType outcomeType, object value){
            OutcomeType = outcomeType;
            Value = value;
        }
    }

    public interface IReadOnlyAutoLootContext{
        IAutoLootableObject Item { get; }
        AutoLootableItemType ItemType { get; }
        LootOutcome LootOutcome { get; }
    }

    internal class AutoLootContext : IReadOnlyAutoLootContext{
        public LootOutcome LootOutcome {get;set;}
        public IAutoLootableObject Item {get;set;}
        public AutoLootableItemType ItemType {get;set;}

        public T GetItem<T>() where T : IAutoLootableObject{
            if(Item is T t){
                return t;
            }
            return default;
        }
    }
}