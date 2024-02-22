using System;
using UnityEditor;
namespace Project.SpawnSystem
{
    [CustomPropertyDrawer(typeof(SpawnScheduleData))]
    public class SpawnScheduleDataDrawer : PropertyDrawer{
        private SerializedProperty m_spawnTrigger;
        private SerializedProperty m_hasRepeat;

        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label){
            m_spawnTrigger = property.FindPropertyRelative("spawnTriggerType");
            m_hasRepeat = property.FindPropertyRelative("hasRepeat");

            switch (m_spawnTrigger.enumValueIndex){
                case 0: // TimeBased
                    DrawTimeBasedData(property);
                break;
                case 1: // EventBased
                    DrawEventBasedData(property);
                break;
            }

            //TOggle for repeat functionality

            m_hasRepeat.boolValue = EditorGUILayout.Toggle("Repeat",m_hasRepeat.boolValue);
            if(m_hasRepeat.boolValue == true){
                //Draw properties like interval time
                EditorGUILayout.BeginFoldoutHeaderGroup(m_hasRepeat.boolValue,"Repeat Settings");
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(property.FindPropertyRelative("intervalTime"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("repeatCondition"), new UnityEngine.GUIContent("Repeat Condition"));
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUI.indentLevel--;
            }
        }

        private void DrawEventBasedData(SerializedProperty source)
        {
            throw new NotImplementedException();
        }

        private void DrawTimeBasedData(SerializedProperty source)
        {
            var spawnTime = source.FindPropertyRelative("spawnTime");
            EditorGUILayout.PropertyField(spawnTime);
        }
    }
}