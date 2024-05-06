#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Project.GameEventSystem.EventCommand;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventFSMNodeBuilder : IEventNodeBuilder
    {
        private readonly List<string> availableVoidCommands;
        private readonly List<string> availableResultCommands;
        private IEventCommandProvider commandProvider;
        public EventFSMNodeBuilder(IEventCommandProvider commandProvider)
        {
            this.commandProvider = commandProvider;
            availableVoidCommands = new List<string>(commandProvider.GetVoidCommandNames());
            availableResultCommands= new List<string>(commandProvider.GetResultCommandNames());
        }
        public EventNode Build(EventNodeData data)
        {
            if (data is EventStateCommandNodeData commandNodeData)
            {
                return BuildEventCommandNodeData(commandNodeData);
            }
            else if(data is EventStateConditionNodeData conditionNodeData){
                return BuildEventConditionNodeData(conditionNodeData);
            }

            //TODO: Add Event State Condition Data
            return null;
        }

        #region Event Command Node
        private void CreatePortsForCommand(EventNode node)
        {
            Port inputPort = EventNodeBuilderFactory.CreatePort(node, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            Port outputPort = EventNodeBuilderFactory.CreatePort(node, Direction.Output, Port.Capacity.Multi);
            outputPort.portName = "OnComplete";
            node.outputContainer.Add(outputPort);
        }
        private EventNode BuildEventCommandNodeData(EventStateCommandNodeData data)
        {
            EventCommandNodeController controller = new EventCommandNodeController();
            
            EventCommandStateNode node = new()
            {
                title = data.name,
                id = data.Id,
                MakeItem = () => new Foldout() { style = { marginTop = 5, marginBottom = 5 }, value = false },
            };
            node.ItemsSource = data.Items;
            controller.SetNodeTarget(node);
            controller.SetNodeDataSource(data);

            CreatePortsForCommand(node);
            node.Insert(1, CreateLabelOption(controller));

            node.CommandDropDown = CreateDropDownForCreatingCommand(
                defaultChoice: "Choose a command", 
                commands: availableVoidCommands, 
                (evt) =>
                {
                    node.CommandDropDown.SetValueWithoutNotify("Choose a command");
                    OnDropdownCommandValueChanged(controller, evt.newValue);
                }
            );
            
            //CreateCommandPropertyBoxes(node, data);
            return node;
        }

        private VisualElement CreateLabelOption(EventCommandNodeController controller)
        {
            VisualElement labelGroup = new();
            labelGroup.style.flexDirection = FlexDirection.Row;
            Toggle markAsLabel = new("Label")
            {
                value = controller.LabelMark,
            };
            Label label = markAsLabel.Q<Label>();
            label.style.minWidth = 50;
            labelGroup.Add(markAsLabel);

            markAsLabel.RegisterValueChangedCallback((evt) =>
            {
                bool isMarked = evt.newValue;
                controller.LabelMark = isMarked;
                controller.SaveData();

                if (isMarked == true)
                {
                    TextField textField = new()
                    {
                        name = "label-name",
                        style = {flexGrow = 1}
                    };
                    textField.SetValueWithoutNotify(controller.Label);
                    textField.RegisterValueChangedCallback((evt) =>
                    {
                        controller.Label = evt.newValue;
                        controller.SaveData();
                    });
                    labelGroup.Insert(1, textField);
                }
                else
                {
                    if(labelGroup.childCount > 1){
                        labelGroup.RemoveAt(1);
                    }
                }

            });

            bool isMarked = controller.LabelMark;
            if (isMarked == true)
            {
                TextField textField = new()
                {
                    name = "label-name",
                    style = {flexGrow = 1}
                };
                textField.SetValueWithoutNotify(controller.Label);
                textField.RegisterValueChangedCallback((evt) =>
                {
                    controller.Label = evt.newValue;
                    controller.SaveData();
                });
                labelGroup.Insert(1, textField);
            }
            else
            {
                if(labelGroup.childCount > 1){
                    labelGroup.RemoveAt(1);
                }
            }

            return labelGroup;
        }

        private void OnDropdownCommandValueChanged(EventCommandNodeController controller, string newValue)
        {
            controller.AppendCommand(new CommandPlaceholderNodeItem(commandProvider.GetCommandId(newValue), newValue));
        }

        private DropdownField CreateDropDownForCreatingCommand(string defaultChoice, List<string> commands, EventCallback<ChangeEvent<string>> onValueChanged)
        {
            DropdownField dropdownField = new DropdownField();
            dropdownField.choices.Add(defaultChoice);
            dropdownField.choices.AddRange(commands);
            dropdownField.SetValueWithoutNotify(defaultChoice);
            dropdownField.RegisterValueChangedCallback(onValueChanged);
            return dropdownField;
        }
        #endregion
        #region Event Condition State Node

        private EventNode BuildEventConditionNodeData(EventStateConditionNodeData data){
            EventConditionNodeController controller = new EventConditionNodeController();
            
            EventConditionStateNode node = new()
            {
                title = data.name,
                id = data.Id,
                MakeItem = () => new Foldout() { style = { marginTop = 5, marginBottom = 5 }, value = false },
            };
            node.ItemsSource = data.Items;
            controller.SetNodeTarget(node);
            controller.SetNodeDataSource(data);

            //node.Insert(1, CreateLabelOption(controller));

            node.ConditionDropdown = CreateDropDownForCreatingCondition(
                defaultChoice: "Add a condition", 
                commands: availableResultCommands,
                (evt) =>
                {
                    node.ConditionDropdown.SetValueWithoutNotify("Add a condition");
                    OnDropdownConditionValueChanged(controller, evt.newValue);
                }
            );
            
            //CreateCommandPropertyBoxes(node, data);
            return node;
        }

        private void OnDropdownConditionValueChanged(EventConditionNodeController controller, string value){
            controller.AddCondition(commandProvider.GetCommandId(value), value);
        }

        private DropdownField CreateDropDownForCreatingCondition(string defaultChoice, List<string> commands, EventCallback<ChangeEvent<string>> onValueChanged)
        {
            DropdownField dropdownField = new DropdownField();
            dropdownField.choices.Add(defaultChoice);
            dropdownField.choices.AddRange(commands);
            dropdownField.SetValueWithoutNotify(defaultChoice);
            dropdownField.RegisterValueChangedCallback(onValueChanged);
            return dropdownField;
        }
        #endregion
    }
}
#endif