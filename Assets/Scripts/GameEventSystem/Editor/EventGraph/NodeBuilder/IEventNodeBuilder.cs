#if UNITY_EDITOR
namespace Project.GameEventSystem.EventGraph.Editor
{
    public interface IEventNodeBuilder
    {
        EventNode Build(EventNodeData data);
    }
}
#endif