using System;
using System.Collections.Generic;
using Project.CharacterBehaviour;

namespace Project.Enemy
{
    public interface IEnemyStateObservable : ICoreComponent{
        void AddDeathEventObserver(IEnemyDeathObserver observer);
        void RemoveDeathEventObserver(IEnemyDeathObserver observer);

        void SubscribeToDectectionState(Action<Core> callback);
        void UnsubscribeToDectectionState(Action<Core> callback);
    }
    public interface IEnemyStateInvoker : ICoreComponent{
        void NotifyDeathEvent(EnemyDeathData data);
        void NotifyPlayerIsDetected(Core player);
    }
    public class EnemyNotifier : IEnemyStateObservable, IEnemyStateInvoker
    {
        private EnemyCore m_core;
        public EnemyNotifier(EnemyCore core){
            if(core == null){
                throw new NullReferenceException("core is null");
            }
            this.m_core = core;
        }
        private List<IEnemyDeathObserver> m_deathObservers = new List<IEnemyDeathObserver>();
        public void AddDeathEventObserver(IEnemyDeathObserver observer){
            if(observer == null) return;
            m_deathObservers.Add(observer);
        }
        public void RemoveDeathEventObserver(IEnemyDeathObserver observer){
            if(observer == null) return;
            m_deathObservers.Remove(observer);
        }

        public void NotifyDeathEvent(EnemyDeathData data){
            foreach(var observer in m_deathObservers){
                observer.OnDead(data);
            }
        }

        public TComponent GetCoreComponent<TComponent>() where TComponent : ICoreComponent
        {
            return m_core.GetComponent<TComponent>();
        }

        public void AddCoreComponent<TComponent>(TComponent component) where TComponent : ICoreComponent
        {
            m_core.AddCoreComponent(component);
        }

        #region Detection State
        private event Action<Core> m_detectionStateEventHandler;
        public void NotifyPlayerIsDetected(Core player)
        {
            m_detectionStateEventHandler?.Invoke(player);
        }

        public void SubscribeToDectectionState(Action<Core> callback)
        {
            m_detectionStateEventHandler += callback;
        }

        public void UnsubscribeToDectectionState(Action<Core> callback)
        {
            m_detectionStateEventHandler -= callback;
        }
        #endregion Detection State
    }
}