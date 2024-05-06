#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventGraphView : GraphView{
        private readonly static Vector2 DefaultNodeSize = new(100,200);

        private Edge[] Edges => this.edges.ToArray();
        private EventNode[] Nodes => this.nodes.Cast<EventNode>().ToArray();

        public EventGraphView(){
            var gridBG = new GridBackground();
            Insert(0, gridBG);
        }

        public void ClearGraph(){
            foreach(var node in Nodes){
                RemoveElement(node);
            }
            foreach(var edge in Edges){
                RemoveElement(edge);
            }
        }

        public override System.Collections.Generic.List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter){
            return ports.Where(port => port != startPort && port.node != startPort.node).ToList();
        }

        private EventNode CreateRootNode(string name)
        {
            EventNode root = new EventNode(){
                title = name,
                id = Guid.Empty
            };
            root.AddToClassList("root");
            Port port = CreatePort(root, Direction.Output, Port.Capacity.Multi);
            port.portName = "Start";
            root.outputContainer.Add(port);

            root.RefreshExpandedState();
            root.RefreshPorts();
            root.SetPosition(new Rect(Vector2.one * 100, DefaultNodeSize));

            return root;
        }

        private Port CreatePort(EventNode node, Direction direction, Port.Capacity multi)
        {
            return node.InstantiatePort(Orientation.Horizontal, direction, multi, typeof(float));
        }

        internal void AddNode(EventNode node, Vector2 position){
            if(node == null){
                Debug.LogError("Node is null");
                return;
            }

            node.SetPosition(new Rect(position, DefaultNodeSize));
            node.RefreshExpandedState();
            node.RefreshPorts();
            AddElement(node);
        }

        internal void Link(Port from, Port to)
        {
            var edge = new Edge(){
                output = from,
                input = to
            };
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            Add(edge);
        }
    }
}
#endif