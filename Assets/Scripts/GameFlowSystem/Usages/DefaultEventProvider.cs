using System;
using System.Collections.Generic;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.GameFlowSystem.InProject
{
    [CreateAssetMenu(fileName = "EventProvider", menuName = "GameFlowSystem/InProject/EventProvider")]
    public class DefaultEventProvider : EventProvider
    {
        [Serializable]
        public struct SystemEventMapId{
            public GameSystemEventType type;
            [SerializeField, EventID]public int eventId;
        }

        [SerializeField] SystemEventMapId[] m_systemEventMap;
        [SerializeField] ScriptableEventProvider m_projectEventProvider;
        readonly Dictionary<GameSystemEventType, int> m_systemEventMapId = new();


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