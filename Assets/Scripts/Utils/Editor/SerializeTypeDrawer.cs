#if UNITY_EDITOR
using System;
using System.Linq;
using Project.Utils.SerializeType;
namespace UnityEditor{
    [CustomPropertyDrawer(typeof(SerializeType))]
    public class SerializeTypeDrawer : PropertyDrawer{
        TypeFilterAttribute _typeFilter;
        string[] typeNames, typeFullNames;
        
        void Init(){
            if(typeFullNames != null) return;

            _typeFilter = (TypeFilterAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(TypeFilterAttribute));

            Type[] filterTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(_typeFilter == null ? DefaultFilter : _typeFilter.Filter)
            .ToArray();

            typeNames = filterTypes.Select(x => x.ReflectedType == null ? x.Name : $"{x.ReflectedType.Name}.{x.Name}").ToArray();
            typeFullNames = filterTypes.Select(x => x.AssemblyQualifiedName).ToArray();
        }

        static bool DefaultFilter(Type type){
            return !type.IsAbstract && !type.IsInterface && !type.IsGenericType;
        }

        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label){
            Init();

            var typeProperty = property.FindPropertyRelative("_assemblyQualifiedName");

            if(string.IsNullOrEmpty(typeProperty.stringValue)){
                typeProperty.stringValue = typeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }

            int currentIndex = Array.IndexOf(typeFullNames, typeProperty.stringValue);
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

            if(newIndex != currentIndex){
                typeProperty.stringValue = typeFullNames[newIndex];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
#endif