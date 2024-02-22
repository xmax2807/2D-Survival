using System;

namespace Project.GameEventSystem
{
    public interface IEventProvider
    {
        IEventSubscription GetEventSubscription(int id);
        void SubscribeToEvent(int id, Delegate callback);
        void UnsubscribeFromEvent(int id, Delegate callback);
    }

    public interface IEventInvoker{
        void Invoke(int id);
        void Invoke<T>(int id, T data);
        void Invoke(int id, object data);
    }
}