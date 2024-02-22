namespace Project.BuffSystem
{
    public abstract class EffectStatModify : IEffect
    {
        protected StatType m_type;

        public EffectStatModify(StatType type){
            m_type = type;
        }
        public abstract void Apply(ITarget target);

        public abstract void Remove(ITarget target);
    }

    public class PlusEffectStatModify : EffectStatModify
    {
        private int m_amount;
        public PlusEffectStatModify(StatType type, int amount) : base(type){
            m_amount = amount;
        }
        public override void Apply(ITarget target)
        {
            target.StatModifier.IBuffPlus.TryAddPlus(m_type, m_amount);
        }

        public override void Remove(ITarget target)
        {
            target.StatModifier.IBuffPlus.TrySubPlus(m_type, m_amount);
        }
    }

    public class MulEffectStatModify : EffectStatModify{
        private float m_amount;
        public MulEffectStatModify(StatType type, float amount) : base(type){
            m_amount = amount;
        }

        public override void Apply(ITarget target){
            target.StatModifier.IBuffMul.TryAddMul(m_type, m_amount);
        }

        public override void Remove(ITarget target)
        {
            target.StatModifier.IBuffMul.TrySubMul(m_type, m_amount);
        }
    }
}