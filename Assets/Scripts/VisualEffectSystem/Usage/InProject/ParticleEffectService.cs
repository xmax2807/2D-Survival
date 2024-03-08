using System;
using System.Collections.Generic;
using Project.GameDb;
using Project.GameDb.ScriptableDatabase;
using UnityEngine;

namespace Project.VisualEffectSystem.Usage.InProject
{
    public class ParticleEffectService : IParticleEffectService
    {
        readonly Dictionary<int, EffectPool<ParticleSystem>> m_effectCache;
        public event Action<int> OnEffectRequest;
        readonly IVFXRepository m_repository;
        readonly Transform m_container;

        public ParticleEffectService(IVFXRepository repository, Transform container){
            m_repository = repository;
            m_container= container;
            m_effectCache = new Dictionary<int, EffectPool<ParticleSystem>>();
        }

        public ParticleSystem Template(int id) => m_repository.GetParticleEffect(id).vfx;

        public void PlayParticleEffectAt(int effectId, Vector2 position, Action OnComplete = null)
        {
            if(!m_effectCache.TryGetValue(effectId, out var effectPool)){
                ParticleSystem template = Template(effectId);
                effectPool = new EffectPool<ParticleSystem>(m_container, template);
                m_effectCache[effectId] = effectPool;
            }

            var effect = effectPool.Get();
            effect.transform.position = position;
            effect.Play();
            OnComplete?.Invoke();
        }

        public void PlayParticleEffectAt(int effectId, Func<Vector2> trackPosition, Action OnComplete = null)
        {
            
        }
    }
}