using Project.AnimationEventSystem;
using UnityEngine;
namespace UnityEditor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(AnimationEventIDAttribute))]
    public class AnimationEventIDDrawer : PropertyDrawer
    {
        private int chosenIndex;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                chosenIndex = AnimationEventDb.FindIndex(property.intValue);

                chosenIndex = EditorGUI.Popup(position, label.text, chosenIndex, AnimationEventDb.GetMapIdStrings());

                if (chosenIndex >= 0 && chosenIndex != property.intValue)
                {
                    property.intValue = AnimationEventDb.GetIdAt(chosenIndex);
                }
            }
        }
    }
    #endif
}