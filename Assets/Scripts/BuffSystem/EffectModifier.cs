using System.Collections.Generic;

namespace Project.BuffSystem
{
    public interface IEffectModifier{
        IModifierContext Context {get;}
        EffectModifierData GetData();
    }
    public class EffectModifierData{
        public IEffect[] Effects {get;private set;}
        public IModifierContext Context {get;private set;}
        public EffectModifierData(IEffect[] effects, IModifierContext context){
            Effects = effects;
            Context = context;
        }
    }
    public class EffectModifier : IEffectModifier{
        private IEffect[] m_effects;
        public IModifierContext Context {get;private set;}
        private EffectModifierData m_cacheData;

        public void Apply(ITarget m_target)
        {
            for(int i = 0; i < m_effects.Length; ++i){
                m_effects[i].Apply(m_target);
            }
        }

        public EffectModifierData GetData()
        {
            m_cacheData ??= new EffectModifierData(m_effects, Context);
            return m_cacheData;
        }

        public void AcceptBuilder(EffectBuilder builder){
            Context = builder.GetContext();
            m_effects = builder.GetEffects();
        }
    }
}