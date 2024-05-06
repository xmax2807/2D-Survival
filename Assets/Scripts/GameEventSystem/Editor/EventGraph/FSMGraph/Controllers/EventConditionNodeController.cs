#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventConditionNodeController : EventDataController<EventStateConditionNodeData, EventConditionStateNode> 
    {
        private SerializedProperty Conditions => m_serializedNodeData.FindProperty("m_items");
        private SerializedProperty Ports => m_serializedNodeData.FindProperty("m_outPorts");

        public void AddCondition(int commandId, string commandName)
        {
            Conditions.arraySize++;
            Ports.arraySize++;
            SaveData();
            m_nodeData.AddConditionAt(Conditions.arraySize - 1, commandId, commandName);
            SaveData();
        }

        protected override void OnNodeDataSourceSet()
        {
            if(m_nodeData == null) return;
            m_nodeData.OnItemsChanged += OnDataChanged;
            RefreshPorts();
        }

        protected override void OnNodeDataSourceUnset()
        {
            if(m_nodeData == null) return;
            m_nodeData.OnItemsChanged -= OnDataChanged;
        }

        protected override void OnNodeTargetSet()
        {
            if(m_node == null) return;
            RefreshPorts();
            m_node.AddItemEvent += OnItemAdded;
            m_node.IndexChangeEvent += OnItemIndexChanged;
            m_node.BindItem += BindFoldoutCondition;
            m_node.UnbindItem += UnbindFoldoutCondition;
        }

        protected override void OnNodeTargetUnset()
        {
            if(m_node == null) return;
            m_node.AddItemEvent -= OnItemAdded;
            m_node.IndexChangeEvent -= OnItemIndexChanged;
            m_node.BindItem -= BindFoldoutCondition;
            m_node.UnbindItem -= UnbindFoldoutCondition;
        }

        private void OnDataChanged(){
            m_serializedNodeData = new SerializedObject(m_nodeData);
            m_node.ItemsSource = m_nodeData.Items;
            RefreshPorts();
            m_node.RefreshItems();
            m_node.RefreshExpandedState();
        }

        private void OnItemAdded(System.Collections.Generic.IEnumerable<int> indices){
            foreach(int index in indices){
                //dynamically create output port
                m_node.AddOutputPort(m_nodeData.GetPortName(index));
            }
        }
        private void OnItemIndexChanged(int from, int to){
            OnDataChanged();
        }

        private void BindFoldoutCondition(VisualElement ele, int index){
            Foldout commandHolder = ele as Foldout;
            SerializedProperty command = Conditions.GetArrayElementAtIndex(index);

            //Build command holder
            SerializedProperty expectedResultProp = command.FindPropertyRelative("expectedResult");
            SerializedProperty commandHolderProp = command.FindPropertyRelative("commandHolder");
            SerializedProperty commandId = commandHolderProp.FindPropertyRelative("commandId");
            SerializedProperty commandName = commandHolderProp.FindPropertyRelative("commandName");

            commandHolder.text = $"{commandId.intValue}-{commandName.stringValue}";
            commandHolder.Add(CreateExpectedResultField(expectedResultProp));
            commandHolder.Add(CreateChildPropertyField(commandHolderProp, "parameters"));
        }
        private void UnbindFoldoutCondition(VisualElement ele, int index){
            Foldout commandHolder = ele as Foldout;
            PropertyField field = commandHolder.Q<PropertyField>();
            if(field != null){
                field.Unbind();
            }
            commandHolder.Clear();
        }

        private VisualElement CreateExpectedResultField(SerializedProperty property){
            IntegerField field = new IntegerField(maxLength: 2){
                label = "Expected Result"
            };
            field.RegisterValueChangedCallback((evt)=> {
                property.intValue = evt.newValue;
                SaveData();
            });
            field.SetValueWithoutNotify(property.intValue);
            return field;
        }

        private PropertyField CreateChildPropertyField(SerializedProperty property, string childName){
            SerializedProperty childProperty = property.FindPropertyRelative(childName);
            PropertyField propertyField = new(childProperty);
            propertyField.Bind(property.serializedObject);
            return propertyField;
        }

        private void RefreshPorts(){
            if(m_node == null || m_nodeData == null) return;

            m_node.ClearOutputPort();
            string[] portNames = m_nodeData.GetPortNames();
            UnityEngine.Debug.Log("port changed " + portNames.Length);

            for(int i = 0; i < portNames.Length; i++){
                Debug.Log("port name: " + portNames[i]);
                m_node.AddOutputPort(portNames[i]);
            }
        }
    }
}
#endif