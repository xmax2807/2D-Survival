using System;
namespace Project.GameFlowSystem
{

    /// <summary>
    /// This might be confusing at the first time.
    /// This class will add subscriber by invoking the action subscribe, and vice versa 
    /// this action will get subscriber and add to the real event.
    /// Which means, this class just like a mediator between real event and subscriber
    /// For example, subscribe = (listener) => real event += listener, unsubscribe = (listener) => real event -= listener
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyActionEvent<T>{
        private Action<T> Subscribe;
        private Action<T> Unsubscribe;

        public MyActionEvent(Action<T> subscribe, Action<T> unsubscribe){
            if(subscribe == null || unsubscribe == null) throw new ArgumentNullException();
            Subscribe = subscribe;
            Unsubscribe = unsubscribe;
        }

        public void AddSubscriber(T subscriber){
            Subscribe.Invoke(subscriber);
        }

        public void RemoveSubscriber(T subscriber){
            Unsubscribe.Invoke(subscriber);
        }
    }

    public class ActionWrapper : MyActionEvent<Action>{
        public ActionWrapper(Action<Action> subscribe, Action<Action> unsubscribe) : base(subscribe, unsubscribe){}
    }

    public class ActionWrapper<T> : MyActionEvent<System.Action<T>>{
        public ActionWrapper(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe) : base(subscribe, unsubscribe){}
    }
}