using System;

namespace Project.GameEventSystem
{
    public class PhysicEventSubscription : IEventController
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void Invoke<T>(T data)
        {
            throw new NotImplementedException();
        }

        public void Invoke(object data)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(Delegate callback)
        {
            throw new NotImplementedException();
        }
    }
}