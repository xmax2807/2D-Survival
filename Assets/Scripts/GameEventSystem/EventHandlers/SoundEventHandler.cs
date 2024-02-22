using System;
using Project.AudioSystem;

namespace Project.GameEventSystem
{
    public class SoundEventHandler : EventHandler
    {
        //TODO: Sound database from constructor
        //readonly SoundDatabase soundDatabase;
        readonly Action<SoundEventData> FullPlaySoundCallback;
        readonly Action<int> PlaySoundCallback;

        public SoundEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            FullPlaySoundCallback = HandleFullSoundCallback;
            PlaySoundCallback = HandleSoundCallback;
        }

        protected override void RegisterToAPI(){
            m_eventAPI.PlaySoundEvent.Subscribe(FullPlaySoundCallback);
            m_eventAPI.PlaySoundEvent.Subscribe(PlaySoundCallback);
        }

        protected override void UnregisterFromAPI(){
            m_eventAPI.PlaySoundEvent.Unsubscribe(FullPlaySoundCallback);
        }

        void HandleFullSoundCallback(SoundEventData data){
            //TODO request API to get sound clip then tell AudioManager to play it
        }

        void HandleSoundCallback(int id)
        {
            //TODO request API to get sound clip then tell AudioManager to play it
        }
    }
}