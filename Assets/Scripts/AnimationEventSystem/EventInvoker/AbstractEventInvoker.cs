using UnityEngine;
namespace Project.AnimationEventSystem
{
    public abstract class AbstractEventInvoker : ScriptableObject
    {
        [SerializeField] protected ConverterDb m_converterDb;
        public abstract void Invoke(int id, AnimationEventData data);
    }
}