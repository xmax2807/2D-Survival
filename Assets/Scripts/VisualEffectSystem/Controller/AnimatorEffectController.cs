using System;
using System.Collections;
using UnityEngine;
namespace Project.VisualEffectSystem.Controller
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEffectController : MonoBehaviour, IEffectController
    {
        public event Action<int> ControllerFinishedEvent;
        private Action _onAnimationCompleted;
        private Animator _animator;
        private Transform _transform;
        readonly int hashParamId = Animator.StringToHash("Id");
        private void Awake(){
            _animator = GetComponent<Animator>();
            _transform = transform;
        }

        public void SetIdState(int id){
            _animator.SetInteger(hashParamId, id);
        }
        public void PlayEffectAt(Vector2 position, Action onComplete = null)
        {
            this.transform.position = position;

            if(onComplete != null){
                _onAnimationCompleted = onComplete;
            }
        }

        public void PlayEffectAttachTo(Transform target, Action onComplete = null)
        {
            Coroutine attachCoroutine = StartCoroutine(PlayAnimationAttached(target));
            onComplete += () => {
                StopCoroutine(attachCoroutine);
            };

            _onAnimationCompleted = onComplete;
        }

        void OnDisable(){
            _animator.SetInteger(hashParamId, 0);

            if(_onAnimationCompleted != null){
                _onAnimationCompleted.Invoke();
                _onAnimationCompleted = null;
            }
        }


        /// <summary>
        /// Called from Animation Event
        /// </summary>
        public void OnAnimationCompleted(){
            _animator.SetInteger(hashParamId, 0);

            if(_onAnimationCompleted != null){
                _onAnimationCompleted.Invoke();
                _onAnimationCompleted = null;
            }

            ControllerFinishedEvent?.Invoke(GetInstanceID());
        }

        IEnumerator PlayAnimationAttached(Transform target){
            while(true){
                _transform.position = target.position;
                yield return null;
            }
        }

        public override bool Equals(object other)
        {
            if(other is AnimatorEffectController controller){
                return controller.GetInstanceID() == GetInstanceID();
            }
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return GetInstanceID();
        }
    }
}