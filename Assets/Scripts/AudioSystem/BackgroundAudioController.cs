using UnityEngine;

namespace Project.AudioSystem
{
    public class BackgroundAudioController
    {
        private readonly AudioSource _audioSource;
        public BackgroundAudioController(AudioSource audioSource){
            _audioSource = audioSource;
        }
        public void ChangeBackgroundMusic(AudioClip clip, float volume){
            _audioSource.volume = volume;
            _audioSource.clip = clip;
        }
    }
}