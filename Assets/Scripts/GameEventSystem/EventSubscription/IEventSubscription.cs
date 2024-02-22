using System;

namespace Project.GameEventSystem
{
    public interface IEventSubscription{
        void Subscribe(Delegate callback);
        void Unsubscribe(Delegate callback);
    }

    public interface IInvokableEvent{
        void Invoke();
        void Invoke<T>(T data);
        void Invoke(object data);
    }
    public interface IEventController : IEventSubscription, IInvokableEvent
    {
    }
}