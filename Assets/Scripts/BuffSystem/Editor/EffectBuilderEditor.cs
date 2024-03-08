using UnityEditor;
using UnityEngine;
namespace Project.BuffSystem
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(EffectBuilder))]
    public class EffectBuilderEditor : Editor
    {
        SerializedProperty m_metaInfo;
        SerializedProperty m_effect;
        Editor m_effectEditor;
        private string effectName;
        void OnEnable()
        {
            m_metaInfo = serializedObject.FindProperty("MetaInfo");
            m_effect = serializedObject.FindProperty("Data");
            m_effectEditor = CreateEditor(m_effect.objectReferenceValue ?? CreateInstance<EffectData>(), typeof(EffectDataEditor));
            var nameField = m_metaInfo.FindPropertyRelative("Name");

            if(nameField != null && nameField.stringValue != ""){
                effectName = nameField.stringValue;
            }
            else{
                effectName = "Effect Name";
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_metaInfo);
            // EditorGUILayout.PropertyField(m_effect);
            // if(m_effect.objectReferenceValue != null){
            //     //Create Editor for Effect
            //     m_effectEditor = CreateEditor(m_effect.objectReferenceValue, typeof(EffectDataEditor));
            // }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Effect Data", EditorStyles.boldLabel);
            m_effectEditor.OnInspectorGUI();

            if (GUILayout.Button("Apply Changes"))
            {
                if (!AssetDatabase.Contains(m_effectEditor.target))
                {
                    AssetDatabase.CreateAsset(m_effectEditor.target, $"Assets/Resources/{effectName}.asset");
                    m_effect.objectReferenceValue = m_effectEditor.target;
                }
                if(effectName != m_effectEditor.target.name){
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(m_effectEditor.target), effectName);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}