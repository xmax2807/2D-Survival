namespace Project.BuffSystem
{
    public interface IMathCalculator<T>{
        T Add(T value1, T value2);
        T Sub(T value1, T value2);
        T Mul(T value1, T value2);
        T Div(T value1, T value2);
    }
    public static class MathProvider
    {
        private static IMathCalculator<uint> _intMathCalculator = new UIntMathCalculator();
        private static IMathCalculator<float> _floatMathCalculator = new FloatMathCalculator();
        public static IMathCalculator<TValue> GetMathCalculator<TValue>(){
            System.Type valueType = typeof(TValue);
            if(valueType == typeof(uint)){
                return (IMathCalculator<TValue>)_intMathCalculator;
            }
            if(valueType == typeof(float)){
                return (IMathCalculator<TValue>)_floatMathCalculator;
            }
            throw new System.InvalidOperationException($"No calculator available for type {typeof(TValue)}");
        }
        

        private class UIntMathCalculator : IMathCalculator<uint>{
            public uint Add(uint value1, uint value2){
                return value1 + value2;
            }
            public uint Sub(uint value1, uint value2){
                return value1 - value2;
            }
            public uint Mul(uint value1, uint value2){
                return value1 * value2;
            }

            public uint Div(uint value1, uint value2)
            {
                if(value2 == 0){
                    throw new System.DivideByZeroException("value2 is zero in div operation");
                }
                return value1 / value2;
            }
        }

        private class FloatMathCalculator : IMathCalculator<float>{
            public float Add(float value1, float value2){
                return value1 + value2;
            }
            public float Sub(float value1, float value2){
                return value1 - value2;
            }
            public float Mul(float value1, float value2){
                return value1 * value2;
            }

            public float Div(float value1, float value2)
            {
                if(value2 == 0){
                    throw new System.DivideByZeroException("value2 is zero in div operation");
                }
                return value1 / value2;
            }
        }
    }
}