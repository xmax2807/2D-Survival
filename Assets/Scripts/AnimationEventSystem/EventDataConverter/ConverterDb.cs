using System.Collections.Generic;
using UnityEngine;
namespace Project.AnimationEventSystem
{
    public abstract class ConverterDb : ScriptableObject
    {
        [SerializeField, AnimationEventID] protected int id_MaterialDetectionEvent;
        [SerializeField, AnimationEventID] protected int id_SoundEvent;

        protected Dictionary<int, IEventDataConverter> m_converterMap;

        private void Init()
        {
            m_converterMap = new Dictionary<int, IEventDataConverter>();
            DefineConverters();
        }
        protected abstract void DefineConverters();

        public object Convert(int id, AnimationEventData data)
        {
            if (m_converterMap == null)
            {
                Init();
            }
            return m_converterMap[id].Convert(data);
        }

        public bool TryConvert(int id, AnimationEventData data, out object result)
        {
            try
            {
                result = Convert(id, data);
            }
            catch (System.NotImplementedException e)
            {
#if UNITY_EDITOR
                Debug.LogError(e.Message);
#endif
                result = null;
            }

            return result != null;
        }
    }
}