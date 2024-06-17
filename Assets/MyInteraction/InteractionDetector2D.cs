using System;
using UnityEngine;
namespace MyInteraction{

    /// <summary>
    /// this includes detection by renting collider and setting the radius based on config
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    internal sealed class InteractionDetector2D : MonoBehaviour, IInteractionDetector{
        [SerializeField] DetectionConfiguration m_detectionConfig = default;
        private event InteractionDetectionEventDelegate DetectionEnterEvent;
        private event InteractionDetectionEventDelegate DetectionExitEvent;

        private CircleCollider2D m_collider;
        private IInteractable _interactable;

        public void AttachInteractable(IInteractable interactable, InteractionDetectionEventDelegate enterCallback = null, InteractionDetectionEventDelegate exitCallback = null)
        {
            _interactable = interactable;

            if(enterCallback != null){
                DetectionEnterEvent += enterCallback;
            }

            if(exitCallback != null){
                DetectionExitEvent += exitCallback;
            }
        }

        public void DetachInteractable(IInteractable interactable, InteractionDetectionEventDelegate enterCallback = null, InteractionDetectionEventDelegate exitCallback = null)
        {
            _interactable = null;

            if(enterCallback != null){
                DetectionEnterEvent -= enterCallback;
            }

            if(exitCallback != null){
                DetectionExitEvent -= exitCallback;
            }
        }

        public bool Contains(IInteractable interactable) => _interactable == interactable;
        void OnEnable(){
            m_collider = GetComponent<CircleCollider2D>();
            m_collider.radius = m_detectionConfig.radius;
            m_collider.offset = m_detectionConfig.offset;
        }

        void OnTriggerEnter2D(Collider2D other){
            DetectionEnterEvent?.Invoke(_interactable, other.gameObject);
        }
        void OnTriggerExit2D(Collider2D other){
            DetectionExitEvent?.Invoke(_interactable, other.gameObject);
        }

        public void SetEnable(bool value) => this.enabled = value;

        [System.Serializable]
        public readonly struct DetectionConfiguration{
            public readonly float radius;
            public readonly UnityEngine.Vector2 offset;
        }
    }
}