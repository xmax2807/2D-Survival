using System.Collections;

namespace Project.GameEventSystem.EventGraph
{
    public abstract class EventReactionItemData : EventNodeItemData
    {
        public abstract IEnumerator React(EventTriggerSource source);
    }
}