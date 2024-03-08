using System;
using System.Collections.Generic;
using Project.GameDb.ScriptableDatabase;
using Project.VisualEffectSystem.Controller;
using UnityEngine;

namespace Project.VisualEffectSystem.Usage.InProject
{
    public class AnimatorEffectService : IAnimatorEffectService
    {
        public event Action<int> OnPlayEffect;
        static EffectPool<AnimatorEffectController> m_controllerPool;
        static Dictionary<int, AnimatorEffectController> m_activeControllers;

        public AnimatorEffectService(AnimatorEffectController template, Transform container){
            m_controllerPool ??= new EffectPool<AnimatorEffectController>(container, template);
            m_activeControllers ??= new();
        }

        public void PlayEffectAt(int stateId, Vector2 position, Action onComplete = null)
        {
            var effect = m_controllerPool.Get();
            m_activeControllers[effect.GetInstanceID()] = effect;

            effect.ControllerFinishedEvent += OnControllerFinish;
            effect.SetIdState(stateId);
            effect.PlayEffectAt(position, onComplete);
            
            OnPlayEffect?.Invoke(stateId);
        }

        public void PlayEffectAt(int stateId, Transform trackPosition, Action onComplete = null)
        {
            var effect = m_controllerPool.Get();
            m_activeControllers[effect.GetInstanceID()] = effect;

            effect.ControllerFinishedEvent += OnControllerFinish;
            effect.SetIdState(stateId);
            effect.PlayEffectAttachTo(trackPosition, onComplete);
            
            OnPlayEffect?.Invoke(stateId);
        }

        private void OnControllerFinish(int controllerId)
        {
            bool hasKey = m_activeControllers.TryGetValue(controllerId, out AnimatorEffectController controller);
            if(hasKey){
                controller.ControllerFinishedEvent -= OnControllerFinish;
                m_activeControllers.Remove(controllerId);

                //return to pool
                m_controllerPool.Return(controller);
            }
        }
    }
}