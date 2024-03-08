using System;
using UnityEditor;
using UnityEngine;
namespace Project.SpawnSystem
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SpawnData))]
    public class SpawnDataDrawer : PropertyDrawer
    {
        SerializedProperty m_spawnType;
        SerializedProperty m_context;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            m_spawnType = property.FindPropertyRelative("_spawnType");
            m_context = property.FindPropertyRelative("_contextData");
            EditorGUILayout.PropertyField(m_spawnType);
            EditorGUILayout.PropertyField(m_context);
            
            switch (m_spawnType.enumValueIndex)
            {
                case 0: // Regular
                    DrawIndividualData(property);
                    break;
                case 1: // Wave
                    DrawWaveData(property);
                    break;
                case 2: // Boss
                    DrawIndividualData(property);
                    DrawPostEventBoss(property);
                    break;
            }
        }

        private void DrawPostEventBoss(SerializedProperty property)
        {
            SerializedProperty m_postEventBoss = property.FindPropertyRelative("_postEventBoss");
            EditorGUILayout.PropertyField(m_postEventBoss);
        }

        private void DrawIndividualData(SerializedProperty source)
        {
            SerializedProperty m_individualData = source.FindPropertyRelative("_individualData");
            EditorGUILayout.LabelField("=====Individual Data=====", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_individualData);
        }

        private void DrawWaveData(SerializedProperty source)
        {
            SerializedProperty m_waveData = source.FindPropertyRelative("_waveData");
            EditorGUILayout.LabelField("=====Wave Data=====", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_waveData);
        }
    }
    #endif
}
