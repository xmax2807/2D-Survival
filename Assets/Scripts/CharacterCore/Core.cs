using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.CharacterBehaviour
{
    public class Core : MonoBehaviour, ICoreComponent
    {
        Dictionary<string, ICoreComponent> m_components;
        public TComponent GetCoreComponent<TComponent>() where TComponent : ICoreComponent
        {
            string type_str = typeof(TComponent).ToString();
            if (m_components == null || !m_components.ContainsKey(type_str)){
                return default;
            }
            return (TComponent)m_components[type_str];
        }

        public void AddCoreComponent<TComponent>(TComponent component) where TComponent : ICoreComponent{
            if(component == null) return;
            m_components??= new Dictionary<string, ICoreComponent>();
            m_components[typeof(TComponent).ToString()] = component;
        }
        public virtual int GetId() => GetInstanceID();
    }
}