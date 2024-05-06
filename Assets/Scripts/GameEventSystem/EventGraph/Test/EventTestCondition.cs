namespace Project.GameEventSystem.EventGraph
{
    public class EventTestCondition : EventConditionItemData
    {
        [UnityEngine.SerializeField] bool m_value;
        public override bool IsSatisfied(EventTriggerSource source) => m_value;
    }
}