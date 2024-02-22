using System;
using System.Collections.Generic;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.AnimationEventSystem
{
    /// <summary>
    /// Bridge class between AnimationEventSystem and Project event
    /// </summary>
    [CreateAssetMenu(fileName = "InProject_EventInvoker", menuName = "AnimationEventSystem/EventInvoker/InProject_EventInvoker")]
    public class InProjectEventInvoker : AbstractEventInvoker
    {
        [Serializable]
        public struct EventIdMap{
            [EventID]public int ProjectId;
            [AnimationEventID] public int AnimationEventId;
        }

        [SerializeField]ScriptableEventProvider m_projectEventProvider;
        [SerializeField] private EventIdMap[] m_animationEventMapId;

        private Dictionary<int, int> m_idMaps;
        public override void Invoke(int id, AnimationEventData data)
        {
            if(m_idMaps == null){
                InitMap();
            }

            if(!m_idMaps.ContainsKey(id)){
                return;
            }
            int project_id = m_idMaps[id];

            if(false == m_converterDb.TryConvert(id, data, out object result)){
                m_projectEventProvider.Invoke(project_id); //empty parameter invoke    
            }
            else{
                m_projectEventProvider.Invoke(project_id, result);
            }
        }

        private void InitMap()
        {
            m_idMaps = new Dictionary<int, int>();
            for(int i = 0; i < m_animationEventMapId.Length; ++i){
                m_idMaps.Add(m_animationEventMapId[i].AnimationEventId, m_animationEventMapId[i].ProjectId);
            }
        }
    }
}