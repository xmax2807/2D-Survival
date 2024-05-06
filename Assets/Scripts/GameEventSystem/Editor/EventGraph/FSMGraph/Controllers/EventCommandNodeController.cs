using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Project.GameEventSystem.EventGraph.Editor
{
    /// <summary>
    /// A class controls a node in FSM graph
    /// </summary>
    public class EventCommandNodeController : EventDataController<EventStateCommandNodeData, EventCommandStateNode>
    {
        private SerializedProperty LabelMarkProp => m_serializedNodeData.FindProperty("markAsLabel");
        private SerializedProperty LabelProp => m_serializedNodeData.FindProperty("LabelName");
        private SerializedProperty Commands => m_serializedNodeData.FindProperty("m_items");
        
        public bool LabelMark {
            get => LabelMarkProp.boolValue;
            set => LabelMarkProp.boolValue = value;
        }
        public string Label {
            get => LabelProp.stringValue;
            set => LabelProp.stringValue = value;
        }

        protected override void OnNodeDataSourceSet()
        {
            if(m_nodeData == null) return;
            m_nodeData.OnItemsChanged += OnDataChanged;
        }

        protected override void OnNodeDataSourceUnset()
        {
            if(m_nodeData == null) return;
            m_nodeData.OnItemsChanged -= OnDataChanged;
        }

        protected override void OnNodeTargetSet()
        {
            if(m_node == null) return;
            m_node.AddItemEvent += OnItemAdded;
            m_node.IndexChangeEvent += OnItemIndexChanged;
            m_node.BindItem += BindFoldoutCommand;
            m_node.UnbindItem += UnbindFoldoutCommand;
        }

        protected override void OnNodeTargetUnset()
        {
            if(m_node == null) return;
            m_node.AddItemEvent -= OnItemAdded;
            m_node.IndexChangeEvent -= OnItemIndexChanged;
            m_node.BindItem -= BindFoldoutCommand;
            m_node.UnbindItem -= UnbindFoldoutCommand;
        }

        internal void AppendCommand(CommandPlaceholderNodeItem commandPlaceholderNodeItem)
        {
            Commands.arraySize++;
            SaveData();
            m_nodeData.AddCommandHolderAt(Commands.arraySize - 1, commandPlaceholderNodeItem);
            SaveData();
        }

        private void OnDataChanged()
        {
            m_node.RefreshItems();
            m_node.RefreshExpandedState();
            m_serializedNodeData = new SerializedObject(m_nodeData);
            m_node.ItemsSource = m_nodeData.Items;
        }

        private void OnItemAdded(IEnumerable<int> indices)
        {
            foreach (int index in indices)
            {
                m_node.RefreshItem(index);
            }
        }

        private void OnItemIndexChanged(int from, int to)
        {
            OnDataChanged();
            m_node.RefreshItem(from);
            m_node.RefreshItem(to);
        }

        private void BindFoldoutCommand(VisualElement ele, int index){
            UnityEngine.Debug.Log("Binding at index: " + index);
            Foldout commandHolder = ele as Foldout;
            SerializedProperty command = Commands.GetArrayElementAtIndex(index);
            SerializedProperty conditions = command.FindPropertyRelative("conditions");
            
            //Build command holder
            SerializedProperty commandHolderProp = command.FindPropertyRelative("commandHolder");
            SerializedProperty commandId = commandHolderProp.FindPropertyRelative("commandId");
            SerializedProperty commandName = commandHolderProp.FindPropertyRelative("commandName");

            commandHolder.text = $"{commandId.intValue}-{commandName.stringValue}";
            commandHolder.Add(CreateChildPropertyField(commandHolderProp, "parameters"));
        }

        private void UnbindFoldoutCommand(VisualElement commandHolder, int index)
        {
            PropertyField field = commandHolder.Q<PropertyField>();
            if(field != null){
                field.Unbind();
            }
            commandHolder.Clear();
        }

        private PropertyField CreateChildPropertyField(SerializedProperty property, string childName){
            SerializedProperty childProperty = property.FindPropertyRelative(childName);
            PropertyField propertyField = new(childProperty);
            propertyField.Bind(property.serializedObject);
            return propertyField;
        }
    }
}