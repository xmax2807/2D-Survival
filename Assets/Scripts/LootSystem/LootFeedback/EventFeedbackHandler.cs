using System;
using Project.GameEventSystem;
using UnityEngine;

namespace Project.LootSystem
{
    public class EventFeedbackHandler :  ILootFeedbackHandler
    {
        readonly ScriptableEventProvider m_eventProvider;
        readonly int id_eventPlaySound;
        readonly int id_eventPlayVFX;
        readonly int id_eventPopDetailUI;

        public EventFeedbackHandler(ScriptableEventProvider eventProvider, int[] eventIDs){
            m_eventProvider = eventProvider;
            id_eventPlaySound = eventIDs[0];
            id_eventPlayVFX = eventIDs[1];
            id_eventPopDetailUI = eventIDs[2];
        }
        public void PlayFeedback(FeedbackData[] feedbackData, Transform pickerTransform = null){
            for(int i = 0; i < feedbackData.Length; ++i){
                switch(feedbackData[i].Type){
                    case FeedbackType.PlaySound: HandleInvokePlaySound(feedbackData[i], pickerTransform); break;
                    case FeedbackType.PlayVFX: HandleInvokePlayVFX(feedbackData[i], pickerTransform); break;
                    case FeedbackType.PopDetailUI: HandleInvokePopDetailUI(feedbackData[i]); break;
                    default: break;
                }
            }
        }

        private void HandleInvokePopDetailUI(FeedbackData feedbackData)
        {
            throw new NotImplementedException();
        }

        private void HandleInvokePlayVFX(FeedbackData feedbackData, Transform target)
        {
            if(target == null) return;
            
            VisualEffectEventData data = new(feedbackData.vfxId, VisualEffectEventData.EffectType.Animator, target.position);
            m_eventProvider.Invoke(id_eventPlayVFX, data);
        }

        private void HandleInvokePlaySound(FeedbackData feedbackData, Transform _)
        {
            SoundEventData data = new(feedbackData.soundId, feedbackData.volume);
            m_eventProvider.Invoke(id_eventPlaySound, data);
        }
    }
}