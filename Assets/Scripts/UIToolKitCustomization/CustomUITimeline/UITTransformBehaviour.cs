using System;
using UITTimeline;
using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    /// <summary>
    /// A UI Toolkit timeline behaviour to change the transform.
    ///
    /// <see cref="UITTransformClip"/>
    /// </summary>
    [Serializable]
    public class UITTransformBehaviour : UITBehaviour, IAnimatedVisualElementBehaviour
    {
        [SerializeField] private Vector2 anim_position = Vector2.zero;
        [SerializeField] private Vector2 anim_scale = Vector2.zero;
        [SerializeField] private float anim_rotation = 0f;
        [SerializeField] private float anim_opacity = 1f;

        public void Animate(VisualElement element, float weight)
        {
            element.transform.position = anim_position * weight;
            element.transform.rotation = Quaternion.Euler(0f, 0f, anim_rotation * weight);
            element.transform.scale = anim_scale * weight;
            element.style.opacity = anim_opacity * weight;
        }
    }
}