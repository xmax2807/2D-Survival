namespace Project.GameEventSystem.EventGraph
{
    public class EventConditionNodeData : EventNodeData<EventConditionItemData>
    {
        public virtual string TruePortName => "True";
        public virtual string FalsePortName => "False";

        private bool IsSatisfiedAll(EventTriggerSource source)
        {
            foreach(var item in m_items)
            {
                if(!item.IsSatisfied(source))
                {
                    return false;
                }
            }
            return true;
        }
    }
}