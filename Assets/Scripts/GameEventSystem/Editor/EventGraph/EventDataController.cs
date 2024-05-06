using System;
using UnityEditor;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public interface IEventDataController{
        void SetNodeDataSource(EventNodeData source);
        void SetNodeTarget(EventNode node);
        void SaveData();
    }

    public abstract class EventDataController<TNodeData, TEventNode> : IEventDataController
    where TNodeData : EventNodeData
    where TEventNode : EventNode
    {
        protected SerializedObject m_serializedNodeData;
        protected TNodeData m_nodeData;
        protected TEventNode m_node;
        public void SetNodeDataSource(EventNodeData source)
        {
            if(source == null){
                m_nodeData = null;
                OnNodeDataSourceUnset();
                return;
            }
            if(source is not TNodeData nodeData) return;

            OnNodeDataSourceUnset();
            m_nodeData = nodeData;
            m_serializedNodeData = new SerializedObject(m_nodeData);
            OnNodeDataSourceSet();
        }

        protected virtual void OnNodeDataSourceSet(){}
        protected virtual void OnNodeDataSourceUnset() {}

        public void SetNodeTarget(EventNode node)
        {
            if(node == null){
                m_node = null;
                OnNodeTargetUnset();
                return;
            }
            if(node is not TEventNode eventNode) return;
            
            OnNodeTargetUnset();
            m_node = eventNode;
            OnNodeTargetSet();
        }

        protected virtual void OnNodeTargetSet(){}
        protected virtual void OnNodeTargetUnset(){}

        public void SaveData()
        {
            Undo.RecordObject(m_nodeData, "Modify Event Data");
            m_serializedNodeData.ApplyModifiedProperties();
        }
    }
}