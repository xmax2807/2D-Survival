using UnityEditor;
using UnityEngine;
namespace Project.GameEventSystem.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        { 
            string valueStr;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    valueStr = property.intValue.ToString();
                    break;
                case SerializedPropertyType.Boolean:
                    valueStr = property.boolValue.ToString();
                    break;
                case SerializedPropertyType.Float:
                    valueStr = property.floatValue.ToString("0.00000");
                    break;
                case SerializedPropertyType.String:
                    valueStr = property.stringValue;
                    break;
                case SerializedPropertyType.ObjectReference:
                    valueStr = property.objectReferenceValue != null ? property.objectReferenceValue.name : "Null";
                    break;
                default:
                    valueStr = "(not supported type)";
                    break;
            }
            var valueContent = new GUIContent(valueStr);

            float max = EditorStyles.label.CalcSize(valueContent).x * 1.4f;
            float labelMax = EditorStyles.label.CalcSize(label).x * 1.5f;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.MaxWidth(labelMax));
            EditorGUILayout.LabelField(valueContent, GUILayout.MaxWidth(max));
            EditorGUILayout.EndHorizontal();

            property.serializedObject.ApplyModifiedProperties();
        }
    }
}