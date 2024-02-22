using System;
using System.Collections.Generic;
using Project.AudioSystem;
using UnityEngine;
namespace Project.GameEventSystem
{
    [CreateAssetMenu(fileName = "EventStorage", menuName = "GameEventSystem/Storage")]
    public class EventStorage : ScriptableObject
    {
        //Define all events here
        #region GameSystemEvents
        private readonly ActionEventController GameStartEvent = new();
        private readonly ActionEventController SplashScreenCompleted = new();
        private readonly ActionEventController GameEndEvent = new();
        private readonly PlayerEventChangeSubscription PlayerChanged = new();
        private readonly SoundEventSubscription SoundRequestEvent = new();
        private readonly ActionEventControllerT<MaterialDetectionEventData> MaterialDetectionRequestEvent = new();
        #endregion

        [SerializeField] private int id_GameStartEvent;
        [SerializeField] private int id_GameEndEvent;
        [SerializeField] private int id_SplashScreenCompleted;
        [SerializeField] private int id_PlayerChanged;
        [SerializeField] private int id_SoundRequestEvent;
        [SerializeField] private int id_MaterialDetectionRequestEvent;

        private Dictionary<int, IEventController> m_events;

        private void Init()
        {
            m_events = new Dictionary<int, IEventController>
            {
                { id_GameStartEvent, GameStartEvent },
                { id_GameEndEvent, GameEndEvent },
                { id_SplashScreenCompleted, SplashScreenCompleted },
                { id_PlayerChanged, PlayerChanged },
                {id_SoundRequestEvent, SoundRequestEvent},
                {id_MaterialDetectionRequestEvent, MaterialDetectionRequestEvent}
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

        void OnValidate(){
            m_ids = GetIds();
        }
        private static int[] m_ids;
        public int[] GetIds()
        {
            m_ids = new int[]{
                id_GameStartEvent,
                id_GameEndEvent,
                id_SplashScreenCompleted,
                id_PlayerChanged,
                id_SoundRequestEvent,
                id_MaterialDetectionRequestEvent,
            };
            return m_ids;
        }

        public static int GetIdAt(int index) => m_ids[index];
        public static int FindIndex(int id)=> Array.IndexOf(m_ids, id);

        private static string[] m_idStrings;
        public static string[] GetMapIdStrings(){
            m_idStrings ??= new string[]{
                    $"GameStartEvent: {m_ids[0]}",
                    $"GameEndEvent: {m_ids[1]}",
                    $"SplashScreenCompleted: {m_ids[2]}",
                    $"PlayerChanged: {m_ids[3]}",
                    $"SoundRequested: {m_ids[4]}",
                    $"MaterialDetectionRequested: {m_ids[5]}",
                };

            return m_idStrings;
        }
        #endif
    }
}