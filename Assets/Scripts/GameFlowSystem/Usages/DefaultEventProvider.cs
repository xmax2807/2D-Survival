using System;
using System.Collections.Generic;
using Project.GameEventSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(fileName = "EventProvider", menuName = "GameFlowSystem/EventProvider")]
    public class DefaultEventProvider : EventProvider
    {
        [Serializable]
        public struct SystemEventMapId{
            public GameSystemEventType type;
            [SerializeField, EventID]public int eventId;
        }

        [SerializeField] SystemEventMapId[] m_systemEventMap;
        Dictionary<GameSystemEventType, int> m_systemEventMapId = new Dictionary<GameSystemEventType, int>();

        [SerializeField] Project.GameEventSystem.ScriptableEventProvider m_projectEventProvider;


        void OnEnable(){
            if(m_systemEventMap == null){
                return;
            }
            foreach(SystemEventMapId eventId in m_systemEventMap){
                m_systemEventMapId.Add(eventId.type, eventId.eventId);
            }
        }
        void OnDisable(){
            m_systemEventMapId.Clear();
        }

        //TODO get in project real events API
        public override void RegisterToGameEvent(GameSystemEventType eventType, Delegate callback)
        {
            int eventId = m_systemEventMapId[eventType];
            m_projectEventProvider.SubscribeToEvent(eventId, callback);
        }

        public override void UnregisterFromGameEvent(GameSystemEventType eventType, Delegate callback)
        {
            int eventId = m_systemEventMapId[eventType];
            m_projectEventProvider.UnsubscribeFromEvent(eventId, callback);
        }
    }
}