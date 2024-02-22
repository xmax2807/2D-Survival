using System;

namespace Project.GameEventSystem
{
    public class ActionEventController : IEventController
    {
        private event Action _action;

        public void Subscribe(Delegate callback)
        {
            if (callback is Action action)
            {
                _action += action;
            }
        }

        public void Unsubscribe(Delegate callback)
        {
            if (callback is Action action){
                _action -= action;
            }
        }

        #region IEventInvoker
        public void Invoke()
        {
            _action?.Invoke();
        }

        public void Invoke<T>(T _)
        {
            _action?.Invoke();
        }

        public void Invoke(object _)
        {
            _action?.Invoke();
        }
        #endregion IEventInvoker
    }

    public class ActionEventControllerT<T> : IEventController
    {
        private event Action<T> _action;
        private event Action _emptyAction;
        public void Invoke()
        {
            _emptyAction?.Invoke();
        }

        public void Invoke<TData>(TData data)
        {
            if(data is T realData){
                _action?.Invoke(realData);
            }
            else{
                _emptyAction?.Invoke();
            }
        }

        public void Invoke(object data)
        {
            if(data is T realData){
                _action?.Invoke(realData);
            }
            else{
                _emptyAction?.Invoke();
            }
        }

        public void Subscribe(Delegate callback)
        {
            if(callback is Action<T> action){
                _action += action;
            }
            else if(callback is Action empty_action){
                _emptyAction += empty_action;
            }
        }

        public void Unsubscribe(Delegate callback)
        {
            if(callback is Action<T> action){
                _action -= action;
            }
            else if(callback is Action empty_action){
                _emptyAction -= empty_action;
            }
        }
    }
}