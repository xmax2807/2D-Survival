using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyInteraction
{
    internal delegate void InteractionDetectionEventDelegate(IInteractable interactable, GameObject @object);
    internal sealed class InteractionManager : MonoBehaviour{
        // for now, just simple config through serialized fields
        private static InteractionManager m_instance;
        public static InteractionManager Instance => m_instance;

        [SerializeField] byte maxCapacity = 8;
        private InteractionDetectorPool m_interactionDetectorPool;

        private readonly List<InteractionHandler> m_registeredInteractors = new();
        private readonly List<IInteractionDetector> m_interactables = new();

        void Awake(){
            if(m_instance != null && m_instance != this){
                Destroy(gameObject);
            }
            m_instance = this;
            m_interactionDetectorPool = new InteractionDetectorPool(null);
        }

        // void Update(){
        //     for(int i = m_registeredInteractors.Count - 1; i >= 0; --i){
        //         m_registeredInteractors[i].Update();
        //     }
        // }

        void FixedUpdate(){
            for(int i = m_registeredInteractors.Count - 1; i >= 0; --i){
                m_registeredInteractors[i].FixedUpdate();
            }
        }

        public void AddInteractor(IInteractor interactor){
            foreach(var handler in m_registeredInteractors){
                if(handler.Interactor == interactor){
                    return;
                }
            }

            m_registeredInteractors.Add(new InteractionHandler(maxCapacity, interactor));
        }

        public void RemoveInteractor(IInteractor interactor){
            for(int i = 0; i < m_registeredInteractors.Count; ++i){
                if(m_registeredInteractors[i].Interactor == interactor){
                    m_registeredInteractors.RemoveAt(i);
                    return;
                }
            }
        }

        public void RegisterInteractable(IInteractable interactable){
            for(int i = m_interactables.Count - 1; i >= 0; --i){
                if(m_interactables[i].Contains(interactable)) return;
            }

            IInteractionDetector detector = m_interactionDetectorPool.RentADetector();
            detector.AttachInteractable(interactable, OnSomethingEnterDetectionArea, OnSomethingLeaveInteraction);
            m_interactables.Add(detector);
        }

        public void UnregisterInteractable(IInteractable interactable){
            
            for(int i = m_interactables.Count - 1; i >= 0; --i){
                if(m_interactables[i].Contains(interactable)){
                    IInteractionDetector detector = m_interactables[i];
                    detector.DetachInteractable(interactable, OnSomethingEnterDetectionArea, OnSomethingLeaveInteraction);
                    m_interactables.RemoveAt(i);
                    m_interactionDetectorPool.ReturnADetector(detector);
                    return;
                }
            }
        }

        
        private void OnSomethingEnterDetectionArea(IInteractable interactable, GameObject @object)
        {
            // check if object is interactor
            // TODO can have method to find interactor within registered interactor
            // e.g. bool interactor.HasThisObject(@object);
            if(false == @object.TryGetComponent<IInteractor>(out var interactor)){
                return;
            }

            // find interactor in registered interactors
            for(int i = 0; i < m_registeredInteractors.Count; ++i){
                if(m_registeredInteractors[i].Interactor == interactor){
                    m_registeredInteractors[i].AddInteractable(interactable);
                    return;
                }
            }
        }

        private void OnSomethingLeaveInteraction(IInteractable interactable, GameObject @object)
        {
            // check if object is interactor
            //TODO can have method to find interactor within registered interactor
            // e.g. bool interactor.HasThisObject(@object);
            if(false == @object.TryGetComponent<IInteractor>(out var interactor)){
                return;
            }

            // find interactor in registered interactors
            for(int i = 0; i < m_registeredInteractors.Count; ++i){
                if(m_registeredInteractors[i].Interactor == interactor){
                    m_registeredInteractors[i].RemoveInteractable(interactable);
                    return;
                }
            }
        }

        internal void RentDetector(ref IInteractionDetector m_Detector)
        {
            throw new NotImplementedException();
        }

        internal void ReturnDetector(ref IInteractionDetector m_Detector)
        {
            throw new NotImplementedException();
        }
    }
}