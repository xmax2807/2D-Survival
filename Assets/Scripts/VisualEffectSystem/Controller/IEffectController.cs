using System;
using UnityEngine;

namespace Project.VisualEffectSystem.Controller
{
    public interface IEffectController
    {
        void PlayEffectAt(Vector2 position, Action onComplete = null);
        void PlayEffectAttachTo(Transform target, Action onComplete = null);
    }
}