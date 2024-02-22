using Project.BuffSystem;
using Project.Manager;
using UnityEngine;

namespace Project.PlayerBehaviour{
    public class PlayerEffectEventPublisher : MonoBehaviour{
        [SerializeField] private PlayerCore m_player;
        [SerializeField] private int count;
        IEffectEventPublisher effectEventPublisher;
        void Start(){
            effectEventPublisher = GameManager.Instance.EffectEventPublisher;
        }

        void Update(){
            var cache = new EffectAddedEventData(target: m_player,effectId: 1);
            for(int i = 0; i < count; ++i){
                effectEventPublisher.Publish(EffectEventType.AddEffect, ref cache);
            }
        }
    }
}