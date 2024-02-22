using System.Collections.Generic;

namespace Project.AnimationEventSystem
{
    public class AnimationDataPool
    {
        readonly AnimationEventData m_template;
        readonly Queue<AnimationEventData> m_pool;

        public AnimationDataPool(AnimationEventData template){
            m_template = template;
            m_pool = new Queue<AnimationEventData>();
        }

        public AnimationEventData GetData(){
            if(m_pool.Count == 0){
                return m_template.Clone();
            }
            return m_pool.Dequeue();
        }

        public void ReturnData(AnimationEventData data){
            if(data == m_template){ return; }
            m_pool.Enqueue(data);
        }
    }
}