#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph.Editor{
    [CustomPropertyDrawer(typeof(Parameters8))]
    public class Parameter8Drawer : PropertyDrawer{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty count = property.FindPropertyRelative("paramCount");

            // add for entering int field
            int val = EditorGUILayout.IntField(label, count.intValue, GUILayout.Width(200));
            val = Parameters8.EnsureInRange(val);
            for(int i = 1; i <= val; i++){
                SerializedProperty param = property.FindPropertyRelative("param" + i);
                SerializedProperty intVal = param.FindPropertyRelative("value");
                SerializedProperty isDynamic = param.FindPropertyRelative("isDynamic");
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Dynamic", GUILayout.Width(80));
                isDynamic.boolValue = EditorGUILayout.Toggle(isDynamic.boolValue, GUILayout.Width(70));
                EditorGUILayout.LabelField("Value", GUILayout.Width(50));
                intVal.intValue = EditorGUILayout.IntField(intVal.intValue, GUILayout.Width(100));
                EditorGUILayout.EndHorizontal();

                Undo.RecordObject(property.serializedObject.targetObject, "Modify Parameter");
                param.serializedObject.ApplyModifiedProperties();
            }
            count.intValue = val;
            Undo.RecordObject(property.serializedObject.targetObject, "Modify Parameter");
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif