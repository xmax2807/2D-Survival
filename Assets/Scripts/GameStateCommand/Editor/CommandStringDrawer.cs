using Project.GameStateCommand;
using UnityEngine;

namespace UnityEditor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(CommandNameAttribute))]
    public class CommandStringDrawer : PropertyDrawer{
        private int chosenIndex;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(StateCommandProvider.StateNames == null || StateCommandProvider.StateNames.Length == 0){
                
                EditorGUI.HelpBox(position, "No States Found. Please add States to the StateCommandProvider.cs file", MessageType.Error);
                return;
            }
            if(property.propertyType == SerializedPropertyType.String){
                chosenIndex = StateCommandProvider.FindIndex(property.stringValue);
                
                int newChosenIndex = EditorGUI.Popup(position, label.text, chosenIndex, StateCommandProvider.StateNames);
                if(newChosenIndex >= 0 && chosenIndex != newChosenIndex){
                    chosenIndex = newChosenIndex;
                    property.stringValue = StateCommandProvider.StateNames[chosenIndex];
                }
            }
        }
    }
    #endif
}