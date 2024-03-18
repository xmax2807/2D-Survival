using System.Collections;
using Project.VisualEffectSystem;
using UnityEngine;

namespace Project.Manager
{
    public class VFXInitializer : MonoSystemInitializer
    {
        [SerializeField] VisualEffectSystemConfig m_vfxSystemConfiguration;
        protected override IEnumerator InitializeInternal(GameManager manager)
        {
            yield return null;
            var VFXManager = new VisualEffectManager(m_vfxSystemConfiguration);
            manager.AddService<VisualEffectManager>(VFXManager);
        }
    }
}