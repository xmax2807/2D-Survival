using UnityEditor;
using UnityEngine;
namespace Project.SpawnSystem
{
    [CustomPropertyDrawer(typeof(SpawnCondition))]
    public class SpawnConditionDrawer : PropertyDrawer{

        SerializedProperty m_spawnConditionType;
        SerializedProperty m_value;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
                m_spawnConditionType = property.FindPropertyRelative("conditionType");
                m_value = property.FindPropertyRelative("value");
            EditorGUILayout.LabelField(label);
            EditorGUILayout.PropertyField(m_spawnConditionType);
            switch(m_spawnConditionType.enumValueIndex){
                case 0: // LimitDuration
                    EditorGUILayout.PropertyField(m_value, new GUIContent("Duration"));
                break;
                case 1: // LimitTotalCount
                    EditorGUILayout.PropertyField(m_value, new GUIContent("Max Total Count"));
                break;
                case 2: // LimitActiveCount
                    EditorGUILayout.PropertyField(m_value, new GUIContent("Max Active Count"));
                break;
            }
        }
    }
}