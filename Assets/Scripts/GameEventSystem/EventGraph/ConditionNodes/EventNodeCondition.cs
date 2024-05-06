namespace Project.GameEventSystem.EventGraph
{
    public abstract class EventConditionItemData : EventNodeItemData{
        public abstract bool IsSatisfied(EventTriggerSource source);
    }
}