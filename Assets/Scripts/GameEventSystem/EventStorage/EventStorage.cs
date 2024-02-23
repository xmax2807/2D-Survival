using System;
using System.Collections.Generic;
using Project.AudioSystem;
using UnityEngine;
namespace Project.GameEventSystem
{
    [CreateAssetMenu(fileName = "EventStorage", menuName = "GameEventSystem/Storage")]
    public class EventStorage : ScriptableObject
    {
        [Serializable]
        public class EventIds
        {
            [SerializeField] public int id_GameStartEvent;
            [SerializeField] public int id_GameEndEvent;
            [SerializeField] public int id_SplashScreenCompleted;
            [SerializeField] public int id_PlayerChanged;
            [SerializeField] public int id_SoundRequestEvent;
            [SerializeField] public int id_MaterialDetectionRequestEvent;
            [SerializeField] public int id_InventoryItemAddedEvent;
            [SerializeField] public int id_InventoryItemRemovedEvent;

            [Header("Drop events")]
            [SerializeField] public int id_DropGoldEvent;
            [SerializeField] public int id_DropEXPEvent;
            [SerializeField] public int id_DropItemEvent;
        }
        //Define all events here
        #region GameSystemEvents
        private readonly ActionEventController GameStartEvent = new();
        private readonly ActionEventController SplashScreenCompleted = new();
        private readonly ActionEventController GameEndEvent = new();
        private readonly PlayerEventChangeSubscription PlayerChanged = new();
        private readonly SoundEventSubscription SoundRequestEvent = new();
        private readonly ActionEventControllerT<MaterialDetectionEventData> MaterialDetectionRequestEvent = new();
        private readonly ActionEventControllerT<InventoryItemEventData> InventoryItemAddedEvent = new();
        private readonly ActionEventControllerT<InventoryItemEventData> InventoryItemRemovedEvent = new();
        //Drop events
        private readonly ActionEventControllerT<int> DropGoldEvent = new();
        private readonly ActionEventControllerT<int> DropEXPEvent = new();
        private readonly ActionEventControllerT<int[]> DropItemEvent = new();
        #endregion


        [SerializeField] EventIds m_eventIds;
        private Dictionary<int, IEventController> m_events;

        private void Init()
        {
            m_events = new Dictionary<int, IEventController>
            {
                {m_eventIds.id_GameStartEvent, GameStartEvent },
                {m_eventIds.id_GameEndEvent, GameEndEvent },
                {m_eventIds.id_SplashScreenCompleted, SplashScreenCompleted },
                {m_eventIds.id_PlayerChanged, PlayerChanged },
                {m_eventIds.id_SoundRequestEvent, SoundRequestEvent},
                {m_eventIds.id_MaterialDetectionRequestEvent, MaterialDetectionRequestEvent},
                {m_eventIds.id_InventoryItemAddedEvent, InventoryItemAddedEvent},
                {m_eventIds.id_InventoryItemRemovedEvent, InventoryItemRemovedEvent},
                {m_eventIds.id_DropGoldEvent, DropGoldEvent},
                {m_eventIds.id_DropEXPEvent, DropEXPEvent},
                {m_eventIds.id_DropItemEvent, DropItemEvent},
            };
        }

        public IEventController this[int id]
        {
            get
            {
                if (m_events == null)
                {
                    Init();
                }

                if (!m_events.ContainsKey(id))
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"There is no event with id: {id}");
#endif
                    return null;
                }
                return m_events[id];
            }
        }

#if UNITY_EDITOR

        void OnValidate()
        {
            m_ids = GetIds();
            m_idStrings = new string[]{
                    $"GameStartEvent: {m_ids[0]}",
                    $"GameEndEvent: {m_ids[1]}",
                    $"SplashScreenCompleted: {m_ids[2]}",
                    $"PlayerChanged: {m_ids[3]}",
                    $"SoundRequested: {m_ids[4]}",
                    $"MaterialDetectionRequested: {m_ids[5]}",
                    $"InventoryItemAdded: {m_ids[6]}",
                    $"InventoryItemRemoved: {m_ids[7]}",
                    $"DropGoldEvent: {m_ids[8]}",
                    $"DropEXPEvent: {m_ids[9]}",
                    $"DropItemEvent: {m_ids[10]}"
                };
        }
        private static int[] m_ids;
        public int[] GetIds()
        {
            m_ids = new int[]{
                m_eventIds.id_GameStartEvent,
                m_eventIds.id_GameEndEvent,
                m_eventIds.id_SplashScreenCompleted,
                m_eventIds.id_PlayerChanged,
                m_eventIds.id_SoundRequestEvent,
                m_eventIds.id_MaterialDetectionRequestEvent,
                m_eventIds.id_InventoryItemAddedEvent,
                m_eventIds.id_InventoryItemRemovedEvent,
                m_eventIds.id_DropGoldEvent,
                m_eventIds.id_DropEXPEvent,
                m_eventIds.id_DropItemEvent
            };
            return m_ids;
        }

        public static int GetIdAt(int index) => m_ids[index];
        public static int FindIndex(int id) => Array.IndexOf(m_ids, id);

        private static string[] m_idStrings;
        public static string[] GetMapIdStrings() => m_idStrings;
#endif
    }
}