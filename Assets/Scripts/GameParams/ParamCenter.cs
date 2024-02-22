using System.Collections.Generic;

namespace Project.GameParams
{
    public class ParamCenter : IParamCenter, IParamProvider
    {
        readonly Dictionary<string, object> m_params;

        public ParamCenter(){
            m_params = new Dictionary<string, object>();
        }
        public void AddParamAPI<TInterface, TInstance>(TInstance instance)
            where TInterface : class
            where TInstance : TInterface
        {
            string typeString = typeof(TInterface).ToString();

            if(m_params.ContainsKey(typeString)){
                m_params[typeString] = instance;
            }else{
                m_params.Add(typeString, instance);
            }
        }

        public TInterface GetParamAPI<TInterface>()
        {
            string typeString = typeof(TInterface).ToString();
            return (TInterface)m_params[typeString];
        }

    }
}