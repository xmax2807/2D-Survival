using UnityEditor;
using UnityEngine;

namespace Project.SpawnSystem
{
    [CustomPropertyDrawer(typeof(Rate))]
    public class RateDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), label);
        }
    }
}