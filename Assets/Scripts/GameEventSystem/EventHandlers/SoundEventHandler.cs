using System;
using Project.AudioSystem;
using Project.Manager;

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

        public override void RegisterToAPI(){

            m_eventAPI.PlaySoundEvent.Subscribe(FullPlaySoundCallback);
            m_eventAPI.PlaySoundEvent.Subscribe(PlaySoundCallback);
        }

        public override void UnregisterFromAPI(){
            m_eventAPI.PlaySoundEvent.Unsubscribe(FullPlaySoundCallback);
            m_eventAPI.PlaySoundEvent.Unsubscribe(PlaySoundCallback);
        }

        void HandleFullSoundCallback(SoundEventData data){
            //TODO request API to get sound clip then tell AudioManager to play it
            UnityEngine.Debug.Log($"FullSoundCallback: id: {data.SoundId}, volume: {data.Volume}");
        }

        void HandleSoundCallback(int id)
        {
            //TODO request API to get sound clip then tell AudioManager to play it
            var operation = GameManager.RepoProvider.SoundRepository.GetEntity(id);
            operation.Completed += (op) => AudioManager.Instance.PlaySoundFX(op.Result.Clip, 1);
        }
    }
}