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
    }
}