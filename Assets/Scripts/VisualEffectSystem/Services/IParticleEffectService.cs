using System;
using UnityEngine;

namespace Project.VisualEffectSystem
{
    public interface IParticleEffectService
    {
        event Action<int> OnEffectRequest;
        void PlayParticleEffectAt(int effectId, Vector2 position, Action OnComplete = null);
        void PlayParticleEffectAt(int effectId, Func<Vector2> trackPosition, Action OnComplete = null);
    }
}