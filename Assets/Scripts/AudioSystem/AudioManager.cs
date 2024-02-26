using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        const float SOUNDFX_DELAY_TIME = 0.15f;
        private AudioPool _pool;
        private BackgroundAudioController _backgroundController;
        readonly Dictionary<int, float> audioDelay = new Dictionary<int, float>();
        public static AudioManager Instance { get; private set; }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null && Instance != this)
            {
#if UNITY_EDITOR
                Debug.LogError("Another AudioManager instance already exists! -> Destroying new one...");
#endif
                Destroy(this.gameObject);
                return;
            }

            Instance._pool = new AudioPool(Instance.transform, 10);
            Instance._backgroundController = new BackgroundAudioController(Instance._pool.Get());
        }

        void Start()
        {
            StartCoroutine(UpdateDelay());
        }

        public void PlaySoundFX(AudioClip clip, float volume)
        {
            if (clip == null) return;
            //check if audio limit is reached
            int clipHashCode = clip.GetHashCode();
            if (audioDelay.ContainsKey(clipHashCode))
            {
                return;
            }

            clipAdded.Add(clipHashCode);

            AudioSource source = _pool.Get();
            if (source == null) return;

            source.clip = clip;
            source.volume = volume;

            source.Play();
            StartCoroutine(WaitAndReturnToPool(source));
        }

        public void ChangeBackgroundMusic(AudioClip clip, float volume)
        {
            _backgroundController.ChangeBackgroundMusic(clip, volume);
        }

        private IEnumerator WaitAndReturnToPool(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            _pool.Return(source);
        }

        List<int> clipAdded = new();
        IEnumerator UpdateDelay()
        {
            List<int> needToRemove = new();
            while (true)
            {
                for(int i = clipAdded.Count - 1; i >= 0; --i)
                {
                    int clipHashCode = clipAdded[i];

                    if(!audioDelay.ContainsKey(clipHashCode)){
                        audioDelay.Add(clipHashCode, SOUNDFX_DELAY_TIME);
                    }
                    audioDelay[clipHashCode] -= Time.deltaTime;

                    if (audioDelay[clipHashCode] <= 0)
                    {
                        needToRemove.Add(clipHashCode);
                    }
                }

                if (needToRemove.Count > 0)
                {
                    foreach (int clipHashCode in needToRemove)
                    {
                        audioDelay.Remove(clipHashCode);
                        clipAdded.Remove(clipHashCode);
                    }

                    needToRemove.Clear();
                }
                yield return null;
            }
        }
    }
}