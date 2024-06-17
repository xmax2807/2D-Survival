using System;
using UnityEngine;
namespace MyInteraction{
    public abstract class MonoInteractable : MonoBehaviour, IInteractable{

        public virtual Vector3 GetPosition() => transform.position;

        public abstract void Interact(IInteractor interactor);

        public abstract void UpdateLogic(InteractionContext context);

        public abstract void UpdatePhysics(InteractionContext context);

        protected void OnEnable(){
            InteractionManager.Instance.RegisterInteractable(this);
        }

        protected void OnDisable(){
            InteractionManager.Instance.UnregisterInteractable(this);
        }
    }
}