#if UNITY_EDITOR
using System;
using UnityEngine.UIElements;

namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventStateMachineNode : EventNode{
        public event Action<string> OnNameChangedEvent;
        public event Action<EventStateMachineNode> OnDoubleClickEvent;
        readonly TextField m_nameTextField;
        readonly Label m_numberOfStates;
        //TODO show references to other state machines
        public EventStateMachineNode(string name){
            title = name;
            m_nameTextField = new TextField(label: "Name", maxLength: 16, multiline: false, isPasswordField: false, '\0');
            m_nameTextField.SetValueWithoutNotify(name);
            m_nameTextField.RegisterValueChangedCallback(OnNameChanged);
            extensionContainer.Add(m_nameTextField);
            
            m_numberOfStates = new Label();
            extensionContainer.Add(m_numberOfStates);
            RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
        }

        private void OnMouseDownEvent(MouseDownEvent evt)
        {
            UnityEngine.Debug.Log("OnClicked: " + evt.clickCount);
            // double click
            if(evt.clickCount == 2){
                OnDoubleClickEvent?.Invoke(this);
            }
        }

        private void OnNameChanged(ChangeEvent<string> evt){
            title = evt.newValue;
            OnNameChangedEvent?.Invoke(evt.newValue);
        }

        public void SetNumberOfStates(int numberOfStates){
            m_numberOfStates.text = $"Number of states: {numberOfStates}";
        }
    }
}
#endif