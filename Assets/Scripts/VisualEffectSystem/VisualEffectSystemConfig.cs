using UnityEngine;

namespace Project.VisualEffectSystem
{
    public abstract class VisualEffectSystemConfig : ScriptableObject
    {
        public abstract IAnimatorEffectService GetAnimatorEffectService();
        public abstract IParticleEffectService GetParticleEffectService();
    }
}