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
        Dictionary<Type, Action<ISaveable>> m_registry;

        public void BindAllToRegistered(ISaveable[] saveable)
        {
            for (int i = 0; i < saveable.Length; ++i)
            {
                Type saveableType = saveable[i].GetType();
                if (m_registry[saveableType] == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"No binding found for {saveableType}");
#endif
                    continue;
                }
                m_registry[saveableType].Invoke(saveable[i]);
            }
        }

        public void Initialize(Type[] typeNames)
        {
#if UNITY_EDITOR
            Debug.Log("Initializing BindRegistry");
#endif
            m_registry = new();
            for (int i = 0; i < typeNames.Length; ++i)
            {
                m_registry[typeNames[i]] = null;
            }
            OnBindRegistryCreated?.Invoke(this);
        }

        public void Register<TSaveable>(ISaveBind binder) where TSaveable : ISaveable
        {
            Type saveableType = typeof(TSaveable);
            if (m_registry.ContainsKey(saveableType))
            {
                m_registry[saveableType] += binder.Bind;
#if UNITY_EDITOR
                Debug.Log($"Data {saveableType} registered to {binder}");
#endif
                return;
            }

#if UNITY_EDITOR
            Debug.LogWarning($"No Data {saveableType} found in storage");
#endif
        }

        public void Unregister<TSaveable>(ISaveBind binder) where TSaveable : ISaveable
        {
            Type saveableType = typeof(TSaveable);
            if (m_registry.ContainsKey(saveableType))
            {
                m_registry[saveableType] -= binder.Bind;
                return;
            }

#if UNITY_EDITOR
            Debug.LogWarning($"No Data {saveableType} found in storage");
#endif
        }
    }
}