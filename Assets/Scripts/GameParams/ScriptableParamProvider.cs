using UnityEngine;

namespace Project.GameParams
{
    [CreateAssetMenu(fileName = "ParamProvider", menuName = "Params/ScriptableParamProvider")]
    public class ScriptableParamProvider : ScriptableObject, IParamCenter, IParamProvider
    {
        [SerializeField] ScriptableParam[] Params;
        readonly ParamCenter m_paramCollection = new ParamCenter();
        public void AddParamAPI<TInterface, TInstance>(TInstance instance)
            where TInterface : class
            where TInstance : TInterface
        {
            m_paramCollection.AddParamAPI<TInterface, TInstance>(instance);
        }

        public TInterface GetParamAPI<TInterface>() => m_paramCollection.GetParamAPI<TInterface>();

        public void Initialize(){
            foreach(ScriptableParam param in Params){
                param.OnAddedToCenter(this);
            }
        }

        // public void Dispose(){
        //     foreach(ScriptableParam param in Params){
        //         param.OnRemovedFromCenter(this);
        //     }
        // }
    }
}