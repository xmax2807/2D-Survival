using System.Collections;

namespace Project.GameEventSystem.EventGraph
{
    public class EventReactionNodeData : EventNodeData<EventReactionItemData>
    {
        public string CompletePortName => "Complete";

        private IEnumerator React(EventTriggerSource source)
        {
            foreach(var item in m_items)
            {
                yield return item.React(source);
            }
        }
    }
}