using System;

namespace MyInventory{
    public enum InventoryEventType : ushort{
        ItemAdded,
        ItemRemoved,
        InventoryOpened,
        InventoryClosed,
    }

    public interface IInventoryEventHandler{
        void OnAttachedToMapper(IInventoryEventMapper mapper);
        void OnDetachedFromMapper(IInventoryEventMapper mapper);
    }
    
    public interface IInventoryEventMapper : IDisposable{
        void AttachHandler(IInventoryEventHandler eventHandler);
        void DetachHandler(IInventoryEventHandler eventHandler);
        void SubscribeToEvent<TEvent>(Action<TEvent> callback) where TEvent : BaseInventoryEventData<TEvent>, new();
        void UnsubscribeFromEvent<TEvent>(Action<TEvent> callback) where TEvent : BaseInventoryEventData<TEvent>, new();
    }
}