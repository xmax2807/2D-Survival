using UnityEditor;

namespace Project.UIToolKit.Editor
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(UITTransformClip))]
    public class UITTransformClipEditor : UnityEditor.Editor
    {
        private SerializedProperty _tranformBehaviour;

        void OnEnable()
        {
            _tranformBehaviour = serializedObject.FindProperty("_template");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_tranformBehaviour);
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}