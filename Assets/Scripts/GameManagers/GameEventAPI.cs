using System.Text;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.Manager
{
    [CreateAssetMenu(fileName = "EventSystem_GameEventAPI", menuName = "GameEventSystem/GameEventAPI")]
    public class GameEventAPI : ScriptableObject, IEventAPI
    {
        [SerializeField] private EventStorage m_eventStorage;
        [SerializeField] EventIDList m_eventIds;

        #if UNITY_EDITOR
        private void OnValidate(){
            m_eventIds ??= new EventIDList();
            int[] ids = m_eventStorage.GetIds();
            m_eventIds.Map(ids);
        }
        #endif

        public IEventController GameStartEvent => m_eventStorage[m_eventIds.id_GameStartEvent];
        public IEventController GameEndEvent => m_eventStorage[m_eventIds.id_GameEndEvent];
        public IEventController SplashScreenCompleted => m_eventStorage[m_eventIds.id_SplashScreenCompleted];
        public IEventController PlayerChanged => m_eventStorage[m_eventIds.id_PlayerChanged];

        public IEventController PlaySoundEvent => m_eventStorage[m_eventIds.id_PlaySoundEvent];
        public IEventController MaterialDetectionEvent => m_eventStorage[m_eventIds.id_MaterialDetectionEvent];

        public IEventController InventoryItemAddedEvent => m_eventStorage[m_eventIds.id_InventoryItemAdded];
        public IEventController InventoryItemRemovedEvent => m_eventStorage[m_eventIds.id_InventoryItemRemoved];

        //Drops
        public IEventController DropGoldEvent => m_eventStorage[m_eventIds.id_DropGoldEvent];
        public IEventController DropEXPEvent => m_eventStorage[m_eventIds.id_DropEXPEvent];
        public IEventController DropItemEvent => m_eventStorage[m_eventIds.id_DropItemEvent];
    }
}