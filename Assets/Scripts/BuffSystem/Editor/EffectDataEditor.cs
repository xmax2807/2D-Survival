using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Project.BuffSystem
{
    [CustomEditor(typeof(EffectData))]
    public class EffectDataEditor : Editor
    {
        SerializedProperty m_children;
        SerializedProperty m_effectType;
        SerializedProperty m_modifyStatData;
        //Time tick
        SerializedProperty m_hasTickTime;
        SerializedProperty m_tickTime;
        // deactivate condtion
        SerializedProperty m_deactivateCondition;

        List<Editor> m_childrenEditors;
        private bool isExpanded = true;
        void OnEnable()
        {
            m_children = serializedObject.FindProperty("Children");
            m_hasTickTime = serializedObject.FindProperty("HasTickTime");
            m_tickTime = serializedObject.FindProperty("Tick");
            m_effectType = serializedObject.FindProperty("Type");
            m_modifyStatData= serializedObject.FindProperty("ModifyStat");
            m_deactivateCondition = serializedObject.FindProperty("DeactivateConditionData");
            m_childrenEditors = new List<Editor>();

            for (int i = 0; i < m_children.arraySize; ++i)
            {
                m_childrenEditors.Add(CreateEditor(m_children.GetArrayElementAtIndex(i).objectReferenceValue, typeof(EffectDataEditor)));
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            isExpanded = EditorGUILayout.Foldout(isExpanded, target.name);

            if (isExpanded == false)
            {
                serializedObject.ApplyModifiedProperties();
                return;
            }

            EditorGUI.indentLevel++; // level 1

            EditorGUILayout.PropertyField(m_effectType);
            switch(m_effectType.enumValueIndex)
            {
                case 0: // Modify Stat
                    EditorGUILayout.PropertyField(m_modifyStatData);
                    break;
                case 1: // Remove effect
                    break;
                case 2: // Add new effect
                    break;
                default:
                    break;
            }

            EditorGUILayout.PropertyField(m_deactivateCondition);

            EditorGUILayout.PropertyField(m_hasTickTime);
            if (m_hasTickTime.boolValue == true)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_tickTime);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(m_children.arraySize > 0 ? $"Children: {m_children.arraySize}" : "No child", GUILayout.MaxWidth(200));
            if (GUILayout.Button("Add Child", GUILayout.Width(100)))
            {
                OnAddChild(CreateInstance<EffectData>());
            }
            #region Remove Button
            if (m_children.arraySize <= 0)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button("Remove Child", GUILayout.Width(100)))
            {
                OnRemoveChild(m_children.GetArrayElementAtIndex(m_children.arraySize - 1).objectReferenceValue as EffectData);
            }
            GUI.enabled = true;
            #endregion

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++; // level 2
            for (int i = 0; i < m_children.arraySize; ++i)
            {
                m_childrenEditors[i].OnInspectorGUI();
            }
            EditorGUI.indentLevel--; // level 2
            EditorGUI.indentLevel--; // level 1
            serializedObject.ApplyModifiedProperties();
        }

        private void OnAddChild(EffectData newChild)
        {
            if (newChild == null) return;

            m_children.arraySize++;
            m_childrenEditors.Add(CreateEditor(newChild, typeof(EffectDataEditor)));
            newChild.name = $"{this.target.name}_{m_children.arraySize}";
            AssetDatabase.AddObjectToAsset(newChild, this.target);
            AssetDatabase.SaveAssets();
            m_children.GetArrayElementAtIndex(m_children.arraySize - 1).objectReferenceValue = newChild;
        }

        private void OnRemoveChild(EffectData child)
        {
            if (child == null) return;

            m_children.arraySize--;
            m_childrenEditors.RemoveAt(m_children.arraySize);
            AssetDatabase.RemoveObjectFromAsset(child);
            string path = AssetDatabase.GetAssetPath(child);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }
    }
}