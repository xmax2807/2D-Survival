using UnityEngine;

namespace Project.GameParams
{
    [CreateAssetMenu(fileName = "MaterialParam", menuName = "Params/MaterialParam")]
    public class ScriptableMaterialParam : ScriptableParam<Enums.MaterialType, MaterialParam>, IMaterialParamAPI
    {
        public int GetSoundId(Enums.MaterialType materialType)
        {
            return this[materialType].StepSoundId;
        }

        public override void OnAddedToCenter(IParamCenter provider)
        {
            provider.AddParamAPI<IMaterialParamAPI, ScriptableMaterialParam>(this);
        }

        protected override void DefineMap()
        {
            for(int i = 0; i < m_params.Length; ++i)
            {
                m_paramMap.Add(m_params[i].MaterialType, m_params[i]);
            }
        }
    }
}