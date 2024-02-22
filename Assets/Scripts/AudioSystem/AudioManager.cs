using System;
using System.Collections;
using Project.AnimationEventSystem;
using Project.Manager;
using UnityEngine;

namespace Project.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        private AudioPool _pool;
        private BackgroundAudioController _backgroundController;
        public static AudioManager Instance { get; private set; }
        void Awake(){
            if(Instance == null){
                Instance = this;
            }
            else if(Instance != null && Instance != this){
                #if UNITY_EDITOR
                Debug.LogError("Another AudioManager instance already exists! -> Destroying new one...");
                #endif
                Destroy(this.gameObject);
                return;
            }

            Instance._pool = new AudioPool(Instance.transform, 10);
            Instance._backgroundController = new BackgroundAudioController(Instance._pool.Get());
        }

        public void PlaySoundFX(AudioClip clip, float volume){
            if(clip == null) return;
            AudioSource source = _pool.Get();
            if(source == null) return;

            source.clip = clip;
            source.volume = volume;

            source.Play();
            StartCoroutine(WaitAndReturnToPool(source));
        }

        public void ChangeBackgroundMusic(AudioClip clip, float volume){
            _backgroundController.ChangeBackgroundMusic(clip, volume);
        }

        private IEnumerator WaitAndReturnToPool(AudioSource source){
            yield return new WaitForSeconds(source.clip.length);
            _pool.Return(source);
        }
    }
}