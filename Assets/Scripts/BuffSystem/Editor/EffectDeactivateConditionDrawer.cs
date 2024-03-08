using UnityEditor;
using UnityEngine;

namespace Project.BuffSystem
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EffectData.DeactivateCondition))]
    public class EffectDeactivateConditionEditor : PropertyDrawer{
        private bool m_foldout = false;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var conditionType = property.FindPropertyRelative("Type");

            m_foldout = EditorGUILayout.Foldout(m_foldout, "Deactivate Condition");
            if(m_foldout == false){
                return;
            }
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(conditionType, new GUIContent("Condition Type"));

            EditorGUI.indentLevel++;
            switch(conditionType.enumValueIndex){
                case 0: break; //None
                case 1: // use Duration
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Duration"), new GUIContent("Duration (sec)")); 
                break;
                case 2: // use Event
                EditorGUILayout.PropertyField(property.FindPropertyRelative("EventType")); 
                break;
            }

            // Rechargable part, the logic is when this effect can be deactivated then it might be recharged
            EditorGUI.indentLevel--;
            if(conditionType.enumValueIndex != 0) // effect can be deactivated
            {
                EditorGUI.indentLevel++;
                //TODO: add when to recharge (immediately after deactivation or delayed or other effect is deactivated)
                EditorGUILayout.PropertyField(property.FindPropertyRelative("RechargeTime"), new GUIContent("Recharge Time (sec)"));
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel = 0;
        }
    }
    #endif
}