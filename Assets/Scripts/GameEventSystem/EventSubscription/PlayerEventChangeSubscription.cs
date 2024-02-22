using System;
using Project.PartitionSystem;

namespace Project.GameEventSystem
{
    public class PlayerEventChangeSubscription : IEventController
    {
        private event Action<ITrackedTarget> m_onTrackedTargetChanged;
        private event Action m_onTargetChanged_action;

        public void Invoke()
        {
            m_onTargetChanged_action?.Invoke();
        }

        public void Invoke<T>(T data)
        {
            if(data is ITrackedTarget target){
                m_onTrackedTargetChanged?.Invoke(target);
            }
            else{
                m_onTargetChanged_action?.Invoke();
            }
        }

        public void Invoke(object data)
        {
            if(data is ITrackedTarget target){
                m_onTrackedTargetChanged?.Invoke(target);
            }
            else{
                m_onTargetChanged_action?.Invoke();
            }
        }

        public void Subscribe(Delegate callback)
        {
            if(callback is Action<ITrackedTarget> action_target)
            {
                m_onTrackedTargetChanged += action_target;
            }
            else if(callback is Action action)
            {
                m_onTargetChanged_action += action;
            }
        }

        public void Unsubscribe(Delegate callback)
        {
            if(callback is Action<ITrackedTarget> action_target){
                m_onTrackedTargetChanged -= action_target;
            }
            else if(callback is Action action){
                m_onTargetChanged_action -= action;
            }
        }
    }
}