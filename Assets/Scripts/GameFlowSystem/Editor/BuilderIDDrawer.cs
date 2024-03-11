#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CustomPropertyDrawer(typeof(BuilderIDAttribute))]
    public class GameFlowSystemBuilderIDDrawer : PropertyDrawer{
        private int chosenIndex;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            if(property.propertyType == SerializedPropertyType.String){
                chosenIndex = SequenceConfiguration.FindIndex(property.stringValue);

                int newIndex = EditorGUI.Popup(position, label.text, chosenIndex, SequenceConfiguration.AvailableBuilders);

                if(newIndex >= 0 && chosenIndex != newIndex){
                    chosenIndex = newIndex;
                    property.stringValue = SequenceConfiguration.AvailableBuilders[chosenIndex];
                }
            }
        }  
    }
}
#endif