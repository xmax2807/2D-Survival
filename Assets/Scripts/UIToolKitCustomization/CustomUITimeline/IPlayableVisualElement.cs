using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    public interface IAnimatedVisualElementBehaviour : UnityEngine.Playables.IPlayableBehaviour
    {
        void Animate(VisualElement target, float inputWeight);
    }
}