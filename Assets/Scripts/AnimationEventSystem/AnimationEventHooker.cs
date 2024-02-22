using UnityEngine;
namespace Project.AnimationEventSystem{
    public class AnimationEventHooker : MonoBehaviour{
        [SerializeField] private AbstractEventInvoker m_eventInvoker;
        [SerializeField] private AnimationEventDb m_animationEventDb;
        public void OnEventTriggered(AnimationEvent animationEvent){
            int event_id = animationEvent.intParameter;
            string event_params = animationEvent.stringParameter;

            AnimationEventData animationEventData = m_animationEventDb.GetDataFromPool(event_id);
            
            animationEventData.Invoker = this.gameObject;
            animationEventData.MapFromString(event_params);
            m_eventInvoker.Invoke(event_id, animationEventData);
            
            m_animationEventDb.ReturnDataToPool(event_id, animationEventData);
        }
    }
}