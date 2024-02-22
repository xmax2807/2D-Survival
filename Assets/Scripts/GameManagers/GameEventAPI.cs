using System.Text;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.Manager
{
    [CreateAssetMenu(fileName = "EventSystem_GameEventAPI", menuName = "GameEventSystem/GameEventAPI")]
    public class GameEventAPI : ScriptableObject, IEventAPI
    {
        [SerializeField] private EventStorage m_eventStorage;
        [SerializeField, EventID] int id_GameStartEvent = -1;
        [SerializeField, EventID] int id_GameEndEvent = -1;
        [SerializeField, EventID] int id_SplashScreenCompleted = -1;
        [SerializeField, EventID] int id_PlayerChanged = -1;
        [SerializeField, EventID] int id_PlaySoundEvent = -1;
        [SerializeField, EventID] int id_MaterialDetectionEvent = -1;

        public IEventController GameStartEvent => m_eventStorage[id_GameStartEvent];
        public IEventController GameEndEvent => m_eventStorage[id_GameEndEvent];
        public IEventController SplashScreenCompleted => m_eventStorage[id_SplashScreenCompleted];
        public IEventController PlayerChanged => m_eventStorage[id_PlayerChanged];

        public IEventController PlaySoundEvent => m_eventStorage[id_PlaySoundEvent];
        public IEventController MaterialDetectionEvent => m_eventStorage[id_MaterialDetectionEvent];
    }
}