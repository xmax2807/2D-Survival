using System;
using UnityEngine;
namespace Project.BuffSystem
{
    [CreateAssetMenu(fileName = "EffectConfiguration", menuName = "EffectSystem/EffectConfiguration")]
    public class ScriptableEffectConfiguration : ScriptableObject, IEffectEventPublisher
    {
        public EffectEventManager EventManager {get;private set;}
        public IEffectEventFactory EventFactory {get;private set;}

        void OnEnable(){
            EventFactory = new EffectEventFactory();
            EventManager = new EffectEventManager(EventFactory);
            InitializeEffects();
        }

        private void InitializeEffects()
        {
        }

        public void Publish<TEventData>(EffectEventType type, ref TEventData eventData)
        {
            EventManager.Publish(type, ref eventData);
        }
    }
}