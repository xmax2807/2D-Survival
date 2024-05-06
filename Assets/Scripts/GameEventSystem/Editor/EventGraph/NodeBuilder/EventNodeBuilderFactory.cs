#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public sealed class EventNodeBuilderFactory
    {
        readonly Dictionary<System.Type, IEventNodeBuilder> m_builders;
        public EventNodeBuilderFactory()
        {
            m_builders = new();
        }

        internal void RegisterBuilder<TNode>(IEventNodeBuilder builder) where TNode : EventNodeData
        {
            m_builders.Add(typeof(TNode), builder);
        }

        internal void UnregisterBuilder<TNode>() where TNode : EventNodeData
        {
            m_builders.Remove(typeof(TNode));
        }
        public EventNode Build(EventNodeData data)
        {
            if(data == null) return null;
            return m_builders[data.GetType()].Build(data);
        }

        public static Port CreatePort(EventNode node, Direction direction, Port.Capacity capacity)
        {
            return node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));
        }
    }
}
#endif