#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventConditionNodeBuilder : DropdownEventNodeBuilder<EventConditionNodeData, EventConditionItemData>
    {
        protected override string DefaultChoice => "Select a condition";
        protected override string Label => "Condition";

        protected override EventConditionItemData CreateItemData(EventConditionNodeData dataHolder, string className)
        {
            var result = (EventConditionItemData)UnityEngine.ScriptableObject.CreateInstance(className);
            if(result == null){
                throw new System.NotImplementedException($"Class {className} not found");
            }

            result.name = className;
            AssetDatabase.AddObjectToAsset(result, dataHolder);
            AssetDatabase.SaveAssets();
            return result;
        }

        protected override void CreatePorts(EventNode node, EventConditionNodeData data)
        {
            Port inputPort = EventNodeBuilderFactory.CreatePort(node, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            Port truePort = EventNodeBuilderFactory.CreatePort(node, Direction.Output, Port.Capacity.Multi);
            truePort.portName = data.TruePortName;
            node.outputContainer.Add(truePort);

            Port falsePort = EventNodeBuilderFactory.CreatePort(node, Direction.Output, Port.Capacity.Multi);
            falsePort.portName = data.FalsePortName;
            node.outputContainer.Add(falsePort);
        }
    }

    public class EventReactionNodeBuilder : DropdownEventNodeBuilder<EventReactionNodeData, EventReactionItemData>
    {
        protected override string DefaultChoice => "Select a reaction";

        protected override string Label => "Reaction";

        protected override EventReactionItemData CreateItemData(EventReactionNodeData targetHolder, string className)
        {
            throw new System.NotImplementedException();
        }

        protected override void CreatePorts(EventNode node, EventReactionNodeData data)
        {
            Port inputPort = EventNodeBuilderFactory.CreatePort(node, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            Port truePort = EventNodeBuilderFactory.CreatePort(node, Direction.Output, Port.Capacity.Multi);
            truePort.portName = data.CompletePortName;
            node.outputContainer.Add(truePort);

        }
    }
}
#endif