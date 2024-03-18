using System;
using Project.AudioSystem;
using Project.GameDb;
using Project.GameDb.ScriptableDatabase;
using Project.Manager;

namespace Project.GameEventSystem
{
    public class SoundEventHandler : EventHandler
    {
        readonly Lazy<ISoundRepository> _lazySoundRepo;
        readonly Action<SoundEventData> FullPlaySoundCallback;
        readonly Action<int> PlaySoundCallback;

        public SoundEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            FullPlaySoundCallback = HandleFullSoundCallback;
            PlaySoundCallback = HandleSoundCallback;

            _lazySoundRepo = new Lazy<ISoundRepository>(() => GameManager.Instance.GetService<IDatabaseRepoProvider>().GetRepository<ISoundRepository>());
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
            GameDb.SoundData soundData = _lazySoundRepo.Value?.GetSound(data.SoundId);
            if (soundData == null) return;
            AudioManager.Instance.PlaySoundFX(soundData.Clip, data.Volume);
        }

        void HandleSoundCallback(int id)
        {
            //TODO request API to get sound clip then tell AudioManager to play it
            GameDb.SoundData data = _lazySoundRepo.Value?.GetSound(id);
            if (data == null) return;
            AudioManager.Instance.PlaySoundFX(data.Clip, 1);
        }
    }
}