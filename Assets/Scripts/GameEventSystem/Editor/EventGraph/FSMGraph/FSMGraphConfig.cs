#if UNITY_EDITOR
using Project.GameEventSystem.EventCommand;

namespace Project.GameEventSystem.EventGraph.Editor
{
    [UnityEngine.CreateAssetMenu(fileName = "FSMGraphConfig", menuName = "EventGraph/FSMGraphConfig", order = 0)]
    public class FSMGraphConfig : ScriptableGraphConfig
    {
        [UnityEngine.SerializeField] FSMCommandTable commandProvider;

        protected override void OnInitializeFactory(EventNodeBuilderFactory builderFactory)
        {
            var FSMNodeBuilder = new EventFSMNodeBuilder(commandProvider);
            builderFactory.RegisterBuilder<EventStateCommandNodeData>(FSMNodeBuilder);
            builderFactory.RegisterBuilder<EventStateConditionNodeData>(FSMNodeBuilder);
            builderFactory.RegisterBuilder<StateDataSource>(new EventStateMachineNodeBuilder());
        }
    }
}
#endif