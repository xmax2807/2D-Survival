using System.Collections.Generic;

namespace Project.BuffSystem
{
    public class CompositeEffect : IEffect{
        private IList<IEffect> m_effects;

        public CompositeEffect(params IEffect[] effects)
        {
            m_effects = new List<IEffect>(effects);
        }

        public static CompositeEffect CreateAnonymousEffect(params IEffect[] effects)
        {
            return new CompositeEffect(effects);
        }

        public void AddEffect(IEffect effect)
        {
            m_effects.Add(effect);
        }

        public void RemoveEffect(IEffect effect)
        {
            m_effects.Remove(effect);
        }

        public void Apply(ITarget target)
        {
            foreach(var effect in m_effects){
                effect.Apply(target);
            }
        }
        public void Remove(ITarget target){
            foreach(var effect in m_effects){
                effect.Remove(target);
            }
        }
    }

    public class LimitGroupEffect : IEffect
    {
        private IEffect[] m_effects;
        private LimitGroupEffect(params IEffect[] effects)
        {
            m_effects = effects;
        }

        public static LimitGroupEffect Create(params IEffect[] effects)
        {
            return new LimitGroupEffect(effects);
        }

        public void Apply(ITarget target)
        {
            for(int i = 0; i < m_effects.Length; ++i){
                m_effects[i].Apply(target);
            }
        }

        public void Remove(ITarget target){
            for(int i = 0; i < m_effects.Length; ++i){
                m_effects[i].Remove(target);
            }
        }
    }
}