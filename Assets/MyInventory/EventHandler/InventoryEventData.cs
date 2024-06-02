using System;

namespace MyInventory{
    public abstract class BaseInventoryEventData<TEvent> : IDisposable where TEvent : BaseInventoryEventData<TEvent>, new()
    {
        // public abstract ushort EventType {get;}
        public void Dispose()
        {
            InventoryEventPool<TEvent>.ReleaseEvent(this as TEvent);
        }

        public static TEvent GetFromPool(){
            return InventoryEventPool<TEvent>.GetEvent();
        }

        public abstract void Init();
    }
    public class InventoryItemEventData : BaseInventoryEventData<InventoryItemEventData>
    {
        public int ItemId {get; private set;}
        public int Amount {get; private set;}
        public override void Init()
        {
            ItemId = -1;
            Amount = 0;
        }

        public static InventoryItemEventData GetFromPool(int itemId, int amount){
            InventoryItemEventData data = GetFromPool();
            data.ItemId = itemId;
            data.Amount = amount;
            return data;
        }
    }

    public class InventoryUIActiveEventData : BaseInventoryEventData<InventoryUIActiveEventData>
    {
        public bool IsActive {get; private set;}
        public float Delay {get; private set;}
        public override void Init()
        {
            IsActive = false;
            Delay = 0f;
        }

        public static InventoryUIActiveEventData GetFromPool(bool isActive, float delay){
            InventoryUIActiveEventData data = GetFromPool();
            data.IsActive = isActive;
            data.Delay = delay;
            return data;
        }
    }
}