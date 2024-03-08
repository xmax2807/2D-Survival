using System;
using UnityEngine;

namespace Project.VisualEffectSystem
{
    public interface IAnimatorEffectService
    {
        event Action<int> OnPlayEffect;
        void PlayEffectAt(int id, Vector2 position, Action onComplete = null);
        void PlayEffectAt(int id, Transform trackPosition, Action onComplete = null);
    }
}