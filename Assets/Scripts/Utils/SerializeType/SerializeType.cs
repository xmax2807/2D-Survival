using System;
using UnityEngine;

namespace Project.Utils.SerializeType
{
    [Serializable]
    public class SerializeType : UnityEngine.ISerializationCallbackReceiver
    {
        [SerializeField] string _assemblyQualifiedName = string.Empty;
        public Type Type { get; private set; }

        public void OnBeforeSerialize()
        {
            if(Type != null){
                _assemblyQualifiedName = Type.AssemblyQualifiedName;
            }
        }

        public void OnAfterDeserialize()
        {
            if(!TryGetType(_assemblyQualifiedName, out Type result)){
                Debug.LogError("Type not found: " + _assemblyQualifiedName);
                return;
            }

            Type = result;
        }

        static bool TryGetType(string assemblyQualifiedName, out Type result)
        {
            if(string.IsNullOrEmpty(assemblyQualifiedName)){
                result = null;
                return false;
            }
            result = Type.GetType(assemblyQualifiedName);
            return result != null;
        }
    }
}