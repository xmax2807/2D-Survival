namespace Project.GameEventSystem
{
    public interface IEventAPI
    {
        IEventController GameStartEvent {get;}
        IEventController GameEndEvent {get;}
        IEventController SplashScreenCompleted {get;}

        IEventController PlaySoundEvent {get;}

        //Physic events
        IEventController MaterialDetectionEvent {get;}

        //Inventory
        IEventController InventoryItemAddedEvent {get;}
        IEventController InventoryItemRemovedEvent {get;}

        //Drops
        IEventController DropGoldEvent {get;}
        IEventController DropEXPEvent {get;}
        IEventController DropItemEvent {get;}
    }
}