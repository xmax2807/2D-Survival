namespace Project.VisualEffectSystem
{
    public class VisualEffectManager
    {
        public IParticleEffectService ParticleEffectService {get; private set;}
        public IAnimatorEffectService AnimatorEffectService {get; private set;}

        public VisualEffectManager(VisualEffectSystemConfig config){
            ChangeConfig(config);
        }

        public void ChangeConfig(VisualEffectSystemConfig config){
            ParticleEffectService = config.GetParticleEffectService();
            AnimatorEffectService = config.GetAnimatorEffectService();
        }
    }
}