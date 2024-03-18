using UnityEngine;
namespace Project.GameEventSystem.Test
{
    public class TestEventInvoker : MonoBehaviour
    {
        [SerializeField, EventID] int m_eventID;
        [SerializeField] ScriptableEventProvider m_eventProvider;

        void Start(){
            m_eventProvider?.Invoke(m_eventID);
        }
    }
}