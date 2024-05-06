using System;

namespace Project.GameEventSystem.EventGraph
{
    public static class ParamParser{
        public static bool IsStaticParam(string param){
            // param contains only number
            for(int i = 0; i < param.Length; i++){
                if(!char.IsDigit(param[i])){
                    return false;
                }
            }
            return true;
        }
        public static bool IsDynamicParam(string param){
            // param that covered by {}, for example: {1} => get param at index 1
            return param.StartsWith('{') && param.EndsWith('}');
        }

        public static int GetStaticParamValue(string param){
            return int.Parse(param);
        }

        public static int GetDynamicParamValue(string param){
            int indexOfBracket = param.IndexOf('{');
            string indexStr = param.Substring(indexOfBracket + 1, param.LastIndexOf('}') - indexOfBracket - 1);
            return int.Parse(indexStr);
        }
    }
}