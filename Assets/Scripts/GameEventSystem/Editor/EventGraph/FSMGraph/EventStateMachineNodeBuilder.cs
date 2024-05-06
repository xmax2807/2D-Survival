#if UNITY_EDITOR
using System;

namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventStateMachineNodeBuilder : IEventNodeBuilder
    {
        public EventNode Build(EventNodeData data)
        {
            if(data is StateDataSource nodeData){
                return BuildEventStateMachineNodeData(nodeData);
            }
            throw new NotImplementedException($"Can't found type of {data?.GetType()} to build");
        }

        private EventNode BuildEventStateMachineNodeData(StateDataSource nodeData)
        {
            EventStateMachineController controller = new EventStateMachineController();
            EventStateMachineNode node = new(name: nodeData.Name)
            {
                id = nodeData.Id
            };
            controller.SetNodeDataSource(nodeData);
            controller.SetNodeTarget(node);
            return node;
        }
    }
}
#endif