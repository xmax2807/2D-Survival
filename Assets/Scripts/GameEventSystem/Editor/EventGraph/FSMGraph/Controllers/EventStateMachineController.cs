#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventStateMachineController : EventDataController<StateDataSource, EventStateMachineNode>{
        SerializedProperty m_StateName;
        protected override void OnNodeTargetSet(){
            if(m_node == null) return;
            m_node.OnDoubleClickEvent += ChangeGraphView;
            m_node.OnNameChangedEvent += OnNameChanged;
            RefreshNodeView();
        }

        protected override void OnNodeTargetUnset(){
            if(m_node == null) return;
            m_node.OnDoubleClickEvent -= ChangeGraphView;
            m_node.OnNameChangedEvent -= OnNameChanged;
        }

        protected override void OnNodeDataSourceSet(){
            if(m_nodeData == null) return;
            m_StateName = m_serializedNodeData.FindProperty(nameof(m_nodeData.Name));
            RefreshNodeView();
        }

        protected override void OnNodeDataSourceUnset(){
            if(m_nodeData == null) return;
            m_StateName = null;
            RefreshNodeView();
        }

        private void ChangeGraphView(EventStateMachineNode _)
        {
            EventGraphEditor window = EditorWindow.focusedWindow as EventGraphEditor;
            if(window != null) { // is current window event graph editor
                window.ChangeDataSource(this.m_nodeData);
            }
        }
        
        private void OnNameChanged(string newName)
        {
            if(m_nodeData == null){
                UnityEngine.Debug.LogError("No node data to change name");
                return;
            }
            m_StateName.stringValue = newName;
            m_nodeData.name = newName;
            SaveData();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// upon node data set => update node view
        /// </summary>
        private void RefreshNodeView(){
            if(m_node == null) return;
            if(m_nodeData == null){
                m_node.SetNumberOfStates(0);
                return;
            }
            m_node.SetNumberOfStates(m_nodeData.StateCount);
        }
    }
}
#endif