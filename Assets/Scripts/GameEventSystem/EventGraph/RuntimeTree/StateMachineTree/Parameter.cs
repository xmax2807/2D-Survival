using System;

namespace Project.GameEventSystem.EventGraph
{
    [System.Serializable]
    public struct Parameter : IEquatable<Parameter>
    {
        [UnityEngine.SerializeField]public int value;
        [UnityEngine.SerializeField]public bool isDynamic;

        public readonly bool Equals(Parameter other) => value == other.value && isDynamic == other.isDynamic;

        public static Parameter Dynamic(int value) => new Parameter { value = value, isDynamic = true };
        public static Parameter Static(int value) => new Parameter { value = value, isDynamic = false };
        public static Parameter Empty()=> new Parameter { value = -1, isDynamic = false };
        public static bool IsEmpty(Parameter param) => param.value == -1;

        public static implicit operator int(Parameter param) => param.value;
    }

    [System.Serializable]
    public class Parameters8 : IEquatable<Parameters8>{
        public const int MAX_PARAM_COUNT = 8;
        public static int EnsureInRange(int value) => Math.Min(Math.Max(0, value), MAX_PARAM_COUNT - 1);
        public Parameter this[int index] => ValueOrEmpty(index);
        [UnityEngine.SerializeField, UnityEngine.Range(0, MAX_PARAM_COUNT - 1)] int paramCount;
        [UnityEngine.SerializeField] Parameter param1;
        [UnityEngine.SerializeField] Parameter param2;
        [UnityEngine.SerializeField] Parameter param3;
        [UnityEngine.SerializeField] Parameter param4;
        [UnityEngine.SerializeField] Parameter param5;
        [UnityEngine.SerializeField] Parameter param6;
        [UnityEngine.SerializeField] Parameter param7;
        [UnityEngine.SerializeField] Parameter param8;

        public bool Equals(Parameters8 other)
        {
            return paramCount == other.paramCount
                && param1.Equals(other.param1)
                && param2.Equals(other.param2)
                && param3.Equals(other.param3)
                && param4.Equals(other.param4)
                && param5.Equals(other.param5)
                && param6.Equals(other.param6)
                && param7.Equals(other.param7)
                && param8.Equals(other.param8);
        }

        public Parameter[] ToArray(){
            Parameter[] result = new Parameter[paramCount];
            // only get from 0 to paramCount - 1
            int i = 0;
            if(i < paramCount) result[i++] = param1;
            if(i < paramCount) result[i++] = param2;
            if(i < paramCount) result[i++] = param3;
            if(i < paramCount) result[i++] = param4;
            if(i < paramCount) result[i++] = param5;
            if(i < paramCount) result[i++] = param6;
            if(i < paramCount) result[i++] = param7;
            if(i < paramCount) result[i++] = param8;
            return result;
        }

        public void ToArrayNonAlloc(Parameter[] result){
            if(result == null){
                throw new ArgumentNullException(nameof(result));
            }
            int count = result.Length;
            for(int i = 0; i < count; i++){
                result[i] = ValueOrEmpty(i);
            }
        }

        private Parameter ValueOrEmpty(int index){
            if(index < 0 || index >= paramCount){
                return Parameter.Empty();
            }
            
            if(index == 0) return param1;
            if(index == 1) return param2;
            if(index == 2) return param3;
            if(index == 3) return param4;
            if(index == 4) return param5;
            if(index == 5) return param6;
            if(index == 6) return param7;
            if(index == 7) return param8;

            return Parameter.Empty();
        }
    }
}