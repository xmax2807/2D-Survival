using System;
using Project.GameEventSystem;
using UnityEngine;
namespace Project.PartitionSystem
{
    [CreateAssetMenu(fileName = "PartitionSystem_EventProvider", menuName = "PartitionSystem/EventProvider", order = 1)]
    public class ScriptableEventProvider : ScriptableObject, IEventProvider
    {
        [SerializeField, EventID] int eventId_trackedTargetChanged;
        [SerializeField] GameEventSystem.ScriptableEventProvider m_projectEvents;

        public event Action<ITrackedTarget> OnTrackedTargetChanged;

        void OnEnable(){
            m_projectEvents.SubscribeToEvent(eventId_trackedTargetChanged, OnTrackedTargetChanged);
        }

        void OnDisable(){
            m_projectEvents.UnsubscribeFromEvent(eventId_trackedTargetChanged, OnTrackedTargetChanged);
        }
    }
}