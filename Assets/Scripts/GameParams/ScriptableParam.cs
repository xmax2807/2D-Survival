using System.Collections.Generic;
using UnityEngine;
namespace Project.GameParams
{
    public abstract class ScriptableParam : ScriptableObject, IParamSubscription
    {
        public abstract void OnAddedToCenter(IParamCenter provider);
    }
    public abstract class ScriptableParam<TIndex, TParam> : ScriptableParam where TParam : class
    {
        [SerializeField] protected TParam[] m_params;
        protected Dictionary<TIndex, TParam> m_paramMap;
        public TParam this[TIndex index] {
            get {
                if(m_paramMap == null){
                    m_paramMap = new Dictionary<TIndex, TParam>();
                    DefineMap();
                }

                return m_paramMap[index];
            }
        }

        protected abstract void DefineMap();
    }
}