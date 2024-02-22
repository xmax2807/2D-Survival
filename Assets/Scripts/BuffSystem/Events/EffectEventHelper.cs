using System;
using System.Collections.Generic;

namespace Project.BuffSystem
{
    public static class EffectEventHelper
    {
        private static EffectEventType[] types = (EffectEventType[])Enum.GetValues(typeof(EffectEventType));
        public static Dictionary<EffectEventType,TValue> CreateDictionaryBaseOnType<TValue>(Func<EffectEventType,TValue> valueFactory){
            if(valueFactory == null){
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Dictionary<EffectEventType,TValue> dict = new Dictionary<EffectEventType,TValue>();
            for(int i = 0; i < types.Length; ++i){
                dict.Add(types[i],valueFactory.Invoke(types[i]));
            }
            return dict;
        }
    }
}