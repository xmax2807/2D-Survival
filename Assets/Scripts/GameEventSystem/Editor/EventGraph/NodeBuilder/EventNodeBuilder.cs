#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public abstract class DropdownEventNodeBuilder<TDataNode, TItemData> : IEventNodeBuilder where TDataNode : EventNodeData where TItemData : EventNodeItemData
    {
        protected abstract string DefaultChoice { get; }
        protected abstract string Label { get; }
        public EventNode Build(EventNodeData data)
        {
            if (data is not TDataNode nodeData) return null;
            EventNode node = new()
            {
                title = this.Label,
                id = nodeData.Id
            };
            CreatePorts(node, nodeData);

            SerializedProperty items = new SerializedObject(nodeData).FindProperty("m_items");
            if(items == null || !items.isArray){
                node.extensionContainer.Add(new Label($"Can't find m_items or it's not array of {typeof(TItemData)}"));
                return node;
            }
            

            var dropDown = CreateDropDown(label: this.Label);
            node.extensionContainer.Add(dropDown);
            dropDown.RegisterValueChangedCallback((evt) =>
            {
                TItemData newItemData = CreateItemData(nodeData, evt.newValue);
                if (newItemData == null) return;

                items.arraySize++;
                items.GetArrayElementAtIndex(items.arraySize - 1).objectReferenceValue = newItemData;
                items.serializedObject.ApplyModifiedProperties();

                CreatePropertyBoxTo(node, items, newItemData);                
                dropDown.SetValueWithoutNotify(this.DefaultChoice);
            }
            );

            if(items.arraySize > 0){ // there are elements saved
                AddPropertyBoxesTo(node, items);
            }

            return node;
        }

        protected abstract TItemData CreateItemData(TDataNode targetHolder, string className);

        private void AddPropertyBoxesTo(EventNode targetHolder, SerializedProperty items)
        {
            for(int i = 0; i < items.arraySize; i++){
                CreatePropertyBoxTo(targetHolder, items, items.GetArrayElementAtIndex(i).objectReferenceValue);
            }
        }

        protected DropdownField CreateDropDown(string label)
        {
            var choices = typeof(TDataNode).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(TItemData))).Select(t => t.Name).ToList();
            var dropDown = new DropdownField(label: label, choices: choices, defaultIndex: 0);
            dropDown.choices.Insert(0, this.DefaultChoice);
            dropDown.SetValueWithoutNotify(this.DefaultChoice);
            return dropDown;
        }

        public void CreatePorts(EventNode node, EventNodeData data){
            CreatePorts(node, data as TDataNode);
        }
        protected abstract void CreatePorts(EventNode node, TDataNode data);

        private void CreatePropertyBoxTo(EventNode targetHolder, SerializedProperty property, UnityEngine.Object elementObj)
        {
            if (elementObj == null || targetHolder == null) return;

            GroupBox box = new();
            box.AddToClassList("property-box");

            Foldout foldout = new()
            {
                text = elementObj.GetType().Name
            };
            foldout.contentContainer.AddToClassList("property-foldout");

            SerializedObject serializedElement = new(elementObj);
            SerializedProperty itr = serializedElement.GetIterator();
            if (itr.NextVisible(true))
            {
                do
                {
                    if (itr.name == "m_Script") continue;
                    var field = new PropertyField(itr);
                    field.Bind(serializedElement);
                    foldout.contentContainer.Add(field);
                }
                while (itr.NextVisible(false));
            }

            Button removeButton = new(() =>
            {
                //loop through property array check if there is an elemen equal to m_element
                int i = 0;
                for (; i < property.arraySize; i++)
                {
                    if (property.GetArrayElementAtIndex(i).objectReferenceValue == elementObj)
                    {
                        break;
                    }
                }
                property.DeleteArrayElementAtIndex(i);
                property.serializedObject.ApplyModifiedProperties();
                AssetDatabase.RemoveObjectFromAsset(elementObj);
                AssetDatabase.SaveAssets();
                targetHolder.extensionContainer.Remove(box);
            })
            {
                text = "X"
            };
            foldout.contentContainer.Add(removeButton);

            box.Add(foldout);
            
            targetHolder.extensionContainer.Add(box);
            targetHolder.RefreshExpandedState();
        }
    }
}
#endif