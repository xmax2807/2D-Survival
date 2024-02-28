using System;
using System.Collections.Generic;
using Project.Utils;
using UnityEngine;

namespace Project.SaveSystem
{
    [CreateAssetMenu(fileName = "BindRegistry", menuName = "SaveSystem/BindRegistry")]
    public class ScriptableBindRegistry : ScriptableObject, IBindRegistry
    {
        public static event Action<IBindRegistry> OnBindRegistryCreated;
        Dictionary<string, Action<ISaveable>> m_registry;

        public void BindAllToRegistered(ISaveable[] saveable){
            for(int i = 0; i < saveable.Length; ++i){
                string saveableName = saveable[i].GetType().Name;
                if(!m_registry.ContainsKey(saveableName)){
                    #if UNITY_EDITOR
                    Debug.LogWarning($"No binding found for {saveableName}");
                    #endif
                    continue;
                }
                m_registry[saveableName]?.Invoke(saveable[i]);
            }
        }

        public void Initialize(string[] typeNames)
        {
            Debug.Log("Initializing BindRegistry");
            m_registry = new();
            for(int i = 0; i < typeNames.Length; ++i){
                m_registry[typeNames[i]] = null;
            }
            OnBindRegistryCreated?.Invoke(this);
        }

        public void Register<TSaveable>(ISaveBind binder) where TSaveable : ISaveable
        {
            string saveableName = typeof(TSaveable).Name;
            if(m_registry.ContainsKey(saveableName)){
                m_registry[saveableName] += binder.Bind;

                Debug.Log($"Data {saveableName} registered to {binder}");
                return;
            }

            #if UNITY_EDITOR
            Debug.LogWarning($"No Data {saveableName} found in storage");
            #endif
        }

        public void Unregister<TSaveable>(ISaveBind binder) where TSaveable : ISaveable
        {
            string saveableName = typeof(TSaveable).Name;
            if(m_registry.ContainsKey(saveableName)){
                m_registry[saveableName] -= binder.Bind;
                return;
            }
            
            #if UNITY_EDITOR
            Debug.LogWarning($"No Data {saveableName} found in storage");
            #endif
        }
    }
}