using Project.GameDb.ScriptableDatabase;
using Project.VisualEffectSystem.Controller;
using UnityEngine;

namespace Project.VisualEffectSystem.Usage.InProject
{
    [CreateAssetMenu(fileName = "InProjectVfxSystemConfig", menuName = "VisualEffectSystem/Configs/InProjectConfig")]
    public class InProjectVfxSystemConfig : VisualEffectSystemConfig
    {
        [SerializeField] AnimatorEffectController m_animatorEffectControllerTemplate;
        [SerializeField] ScriptableDatabaseRepoProvider m_scriptableDatabaseRepoProvider;
        private IParticleEffectService _particleEffectService;
        private IAnimatorEffectService _animatorEffectService;

        public override IAnimatorEffectService GetAnimatorEffectService()
        {
            _animatorEffectService ??= new AnimatorEffectService(
                template: m_animatorEffectControllerTemplate,
                container: null
            );
            return _animatorEffectService;
        }

        public override IParticleEffectService GetParticleEffectService()
        {
            _particleEffectService ??= new ParticleEffectService(
                repository: m_scriptableDatabaseRepoProvider.GetRepository<IVFXRepository>(), 
                container: null
            );
            return _particleEffectService;
        }
    }
}