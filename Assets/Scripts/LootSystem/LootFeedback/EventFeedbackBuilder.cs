using Project.GameEventSystem;
using UnityEngine;

namespace Project.LootSystem
{
    [CreateAssetMenu(fileName = "EventFeedbackBuilder", menuName = "LootSystem/FeedbackBuilder/EventFeedbackBuilder")]
    public class EventFeedbackBuilder : LootFeedbackBuilder
    {
        [SerializeField, EventID] private int id_eventPlaySound;
        [SerializeField, EventID] private int id_eventPlayVFX;
        [SerializeField, EventID] private int id_eventPopDetailUI;
        [SerializeField] private ScriptableEventProvider m_eventProvider;

        public override ILootFeedbackHandler BuildLootFeedbackHandler()
        {
            return new EventFeedbackHandler(m_eventProvider, new int[] { id_eventPlaySound, id_eventPlayVFX, id_eventPopDetailUI });
        }
    }
}