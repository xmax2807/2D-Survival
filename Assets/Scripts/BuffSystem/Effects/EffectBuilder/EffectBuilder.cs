using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.BuffSystem
{
    [CreateAssetMenu(fileName = "EffectBuilder", menuName = "EffectSystem/EffectBuilder")]
    public class EffectBuilder : ScriptableObject{
        [SerializeField] private EffectMetaInfo MetaInfo;
        [SerializeField] private EffectData Data;
        private IEffect m_effect;
        private ModifierContext m_context;

        public IEffectModifier Build(ref EffectData data){
            m_context = new ModifierContext();
            EffectModifier result = new EffectModifier();
            switch(data.Type){
                case EffectData.EffectType.ModifyStat: m_effect = CreateModifyStatEffect(data.ModifyStat);
                break;
            }

            if(data.HasDuration){
                //TODO add this duration to context
            }

            if(data.HasChildren){
                //TODO create decorator effect add both  composite effect from children and the effect created above
            }

            result.AcceptBuilder(builder: this);
            return result;
        }

        internal IModifierContext GetContext()
        {
            return this.m_context;
        }

        internal IEffect[] GetEffects()
        {
            return new IEffect[]{m_effect};
        }

        private IEffect CreateModifyStatEffect(EffectData.ModifyStatData data)
        {
            if(data.Operation == EffectData.ModifyStatData.OperationType.Plus){
                return new PlusEffectStatModify(data.StatType, (int)data.Amount);
            }
            //else == Mul
            return new MulEffectStatModify(data.StatType, data.Amount);
        }
    }
}