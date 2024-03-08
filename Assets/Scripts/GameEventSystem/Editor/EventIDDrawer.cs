using Project.GameEventSystem;
using UnityEngine;

namespace UnityEditor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EventIDAttribute))]
    public class EventIDDrawer : PropertyDrawer{
        private int chosenIndex;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property.propertyType == SerializedPropertyType.Integer){
                chosenIndex = EventStorage.FindIndex(property.intValue);

                chosenIndex = EditorGUI.Popup(position, label.text, chosenIndex, EventStorage.GetMapIdStrings());

                if(chosenIndex >= 0 && chosenIndex != property.intValue){
                    property.intValue = EventStorage.GetIdAt(chosenIndex);
                }
            }
        }
    }
    #endif
}