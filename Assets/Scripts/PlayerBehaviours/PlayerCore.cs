using System;
using Project.BuffSystem;
using Project.GameEvent;
using UnityEngine;
using Project.Manager;
using Project.CharacterBehaviour;

namespace Project.PlayerBehaviour{
    public class PlayerCore : Core, ITarget{
        [SerializeField] PlayerDataController m_playerData;
        private PlayerModifier m_playerStatModifier;
        private PlayerStats m_playerStats;

        public IStatModifier StatModifier => m_playerStatModifier;

        public IStatGetter BaseStateGetter => m_playerStats;

        public IEffectHandler EffectHandler => throw new NotImplementedException();

        private void Awake(){
            AddCoreComponent(m_playerData);
            m_playerStats = new PlayerStats();
            m_playerStatModifier = new PlayerModifier(m_playerStats, new PlayerBuffs());
        }

        void OnEnable(){
            var GameEventRegisterer = GameManager.Instance.GetService<IGameEventRegister>();
            GameEventRegisterer.Register<IEffect>(GameEventType.EffectAdded, OnEffectAdded);
        }

        void OnDisable(){
            var GameEventRegisterer = GameManager.Instance.GetService<IGameEventRegister>();
            GameEventRegisterer.Unregister<IEffect>(GameEventType.EffectAdded, OnEffectAdded);
        }

        private void OnEffectAdded(IEffect effect)
        {
            effect.Apply(this);
        }

        public void AddEffect(IEffect effect)
        {
            throw new NotImplementedException();
        }

        public void RemoveEffect(IEffect effect)
        {
            throw new NotImplementedException();
        }
    }
}