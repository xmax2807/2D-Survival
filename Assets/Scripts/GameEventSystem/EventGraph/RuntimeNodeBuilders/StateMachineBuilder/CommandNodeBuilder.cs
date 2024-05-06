using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.GameEventSystem.EventGraph
{
    public class StateMachineTreeBuilder {
        StateMachineTree result;
        readonly Dictionary<Guid, SMNode> cache = new Dictionary<Guid, SMNode>();
        public StateMachineTree BuildTree(EventNodeData[] nodes, EventLinkData[] links){
            Clear();

            Guid id = new Guid();
            StateCommandNode root = new StateCommandNode(id, id);
            result = new StateMachineTree(root);

            root.AddChild(RecursiveBuildNode(nodes[0], nodes, links));

            return result;
        }

        private SMNode RecursiveBuildNode(EventNodeData data, EventNodeData[] nodes, EventLinkData[] links){
            if(cache.ContainsKey(data.Id)){
                return cache[data.Id];
            }

            SMNode node = null;
            if(data is EventStateCommandNodeData commandNodeData){
                node = CreateCommandState(data.Id, result.Id, commandNodeData);
                EventNodeData[] connectedNodes = FindConnectedNodes(data.Id, nodes, links);
                node.AddChild(RecursiveBuildNode(connectedNodes[0], nodes, links));
            }
            //TODO add other types of nodes


            cache.Add(data.Id, node);
            return node;
        }

        private EventNodeData[] FindConnectedNodes(Guid id, EventNodeData[] nodes, EventLinkData[] links){
            List<EventNodeData> result = new List<EventNodeData>();
            for(int i = 0; i < links.Length; i++){
                if(links[i].InputId == id){
                    result.Add(nodes.First(n => n.Id == links[i].OutputId));
                }
            }

            return result.ToArray();
        }

        private void Clear()
        {
            cache.Clear();
            result = null;
        }

        private StateCommandNode CreateCommandState(Guid id, Guid rootId, EventStateCommandNodeData data){
            var result = new StateCommandNode(id, rootId);

            //TODO add commands here
            IFSMCommand[] commands = new IFSMCommand[data.Items.Length];
            for(int i = 0; i < data.Items.Length; i++){
                commands[i] = new VoidCommand(data.Items[i].commandId, CreateParamGetter(data.Items[i].Parameters.ToArray()));
            }

            result.AddCommand(commands);
            return result;
        }

        private Func<IStateMachineContext, int[]> CreateParamGetter(Parameter[] @params){
            int[] result = new int[@params.Length];
            return (context) =>{
                for(int j = 0; j < @params.Length; j++){
                    if(@params[j].isDynamic){
                        result[j] = context.GetParamValue(this.result.Id, @params[j]);
                    }else{
                        result[j] = @params[j];
                    }
                }
                return result;
            };
            
        }
    }
}