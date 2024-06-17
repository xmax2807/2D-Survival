
using System;

namespace MyInteraction{
    internal interface IInteractionDetector{
        void SetEnable(bool value);
        void AttachInteractable(IInteractable interactable, InteractionDetectionEventDelegate enterCallback = null, InteractionDetectionEventDelegate exitCallback = null);
        void DetachInteractable(IInteractable interactable, InteractionDetectionEventDelegate enterCallback = null, InteractionDetectionEventDelegate exitCallback = null);

        bool Contains(IInteractable interactable); // for search
    }
}