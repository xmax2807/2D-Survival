using System;
using System.Collections;
using UnityEngine;
namespace Project.VisualEffectSystem.Controller
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleEffectController : MonoBehaviour, IEffectController
    {
        private ParticleSystem _particleSystem;
        private Transform _transform;
        void Awake(){
            _particleSystem = GetComponent<ParticleSystem>();
        }
        public void PlayEffectAt(Vector2 position, Action onComplete = null)
        {
            _transform.position = position;
            _particleSystem.Play();
            if(onComplete != null){
                StartCoroutine(WaitForEffect(_particleSystem.main.duration, onComplete));
            }
        }

        public void PlayEffectAttachTo(Transform target, Action onComplete = null)
        {
            StartCoroutine(PlayAnimationAttached(target, onComplete));
        }

        IEnumerator WaitForEffect(float length, Action onComplete){
            if(onComplete == null || length <= 0) yield break;
            yield return new WaitForSeconds(length);
            _particleSystem.Stop();
            onComplete?.Invoke();
        }

        IEnumerator PlayAnimationAttached(Transform target, Action onComplete){
            float length = _particleSystem.main.duration;
            while(length > 0){
                _transform.position = target.position;
                length -= Time.deltaTime;
                yield return null;
            }
            _particleSystem.Stop();
            onComplete?.Invoke();
        }
    }
}