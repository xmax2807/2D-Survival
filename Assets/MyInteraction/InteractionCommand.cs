using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyInteraction{
    internal sealed class InteractionCommand{
        public IInteractable Target {get; set;}
        public void Execute(IInteractor interactor, int inputKeyId){
            Target?.Interact(interactor);
        }
    }

    public sealed class InteractionContext{
        internal Vector3 interactorPosition;
        public readonly IInteractorFeedback Feedback;

        /// <summary>
        /// This Interactor position updated every fixed update
        /// </summary>
        public Vector3 InteractorPosition => interactorPosition;

        internal InteractionContext(IInteractorFeedback feedback){
            Feedback = feedback;
        }
    }

    internal sealed class InteractionHandler{
        public IInteractor Interactor => m_interactor;

        private readonly InteractionContext m_context;
        private readonly IInteractor m_interactor;
        private readonly byte m_capacity;

        private byte m_currentIndex;
        private IInteractable[] m_interactableObjects;
        private IInteractable m_closestInteractable;

        public InteractionHandler(byte capacity, IInteractor interactor){
            m_interactor = interactor ?? throw new ArgumentNullException(nameof(interactor));
            m_capacity = capacity;
            m_context = new InteractionContext(interactor.GetInteractorFeedback());
        }

        public void AddInteractable(IInteractable interactable){
            if(interactable == null || m_currentIndex >= m_capacity) return;
                
            m_interactableObjects ??= new IInteractable[m_capacity];
            m_interactableObjects[m_currentIndex++] = interactable;
        }
        public void RemoveInteractable(IInteractable interactable){
            if(m_interactableObjects == null) return;
            for(int i = 0; i < m_currentIndex; i++){
                if(m_interactableObjects[i] == interactable){
                    // swap with last
                    m_interactableObjects[i] = m_interactableObjects[m_currentIndex - 1];
                    m_interactableObjects[m_currentIndex - 1] = null;
                    m_currentIndex--;
                    break;
                }
            }
        }

        /// <summary>
        /// Execute the interaction when player press a key
        /// TODO: would need more parameters to be passed in the future
        /// </summary>
        /// <param name="inputKey"></param>
        public void ExecuteInteraction(int inputKey){
            m_closestInteractable.Interact(m_interactor);
        }

        public void Update(){
            for(int i = 0; i < m_currentIndex; i++){
                m_interactableObjects[i].UpdateLogic(m_context);
            }
        }
        public void FixedUpdate(){
            Vector3 prevInteractorPos = m_context.interactorPosition;
            Vector3 interactorPos = m_interactor.GetInteractionPosition();
            if(prevInteractorPos != interactorPos){
                m_context.interactorPosition = interactorPos;
                // find closest interactable
                m_closestInteractable = GetClosestInteractable(in interactorPos);
            }

            for(int i = 0; i < m_currentIndex; i++){
                m_interactableObjects[i].UpdatePhysics(m_context);
            }
        }

        /// <summary>
        /// Brute force search for closest interactable
        /// </summary>
        /// <param name="interactorPos"></param>
        /// <returns></returns>
        private IInteractable GetClosestInteractable(in Vector3 interactorPos)
        {
            IInteractable closest = null;
            float closestDist = float.MaxValue;
            for(int i = 0; i < m_currentIndex; i++){
                float dist = Vector3.SqrMagnitude(interactorPos - m_interactableObjects[i].GetPosition());
                if(dist < closestDist){
                    closest = m_interactableObjects[i];
                    closestDist = dist;
                }
            }
            return closest;
        }
    }
}