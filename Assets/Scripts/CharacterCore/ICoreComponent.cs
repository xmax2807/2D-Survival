using UnityEngine;

namespace Project.CharacterBehaviour
{
    public interface ICoreComponent
    {
        TComponent GetCoreComponent<TComponent>() where TComponent : ICoreComponent;
        void AddCoreComponent<TComponent>(TComponent component) where TComponent : ICoreComponent;
    }

    public abstract class MonoCoreComponent : MonoBehaviour, ICoreComponent
    {
        private Core m_CentralizedCore;
        public Core Core
        {
            get
            {
                if (m_CentralizedCore == null)
                {
                    SetCore(transform.root.GetComponent<Core>());
                }
                return m_CentralizedCore;
            }
        }

        protected void Awake()
        {
            AfterAwake();
        }
        public TComponent GetCoreComponent<TComponent>() where TComponent : ICoreComponent
        {
            if (Core == null)
            { // re-check if can't find the core
#if UNITY_EDITOR
                Debug.LogWarning("There is no central core component in this game object");
#endif
                return default;
            }
            return Core.GetCoreComponent<TComponent>();
        }

        protected virtual void AfterAwake() { }

        public void AddCoreComponent<TComponent>(TComponent component) where TComponent : ICoreComponent
        {
            if (m_CentralizedCore == null)
            {
                return;
            }
            m_CentralizedCore.AddCoreComponent(component);
        }

        public void SetCore(Core core)
        {
            if(core == null) return;
            m_CentralizedCore = core;
        }
    }
}