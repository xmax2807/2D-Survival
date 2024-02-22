using System.Collections;
using Project.Manager;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Project.SpawnSystem
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorUpdateOverride : MonoBehaviour
    {
        [SerializeField] AnimatorGraphContainer _animtorContainer;
        private Animator _animator;
        private AnimationPlayableOutput _playableOutput;
        private AnimatorControllerPlayable _controllerPlayable;
        [SerializeField] RuntimeAnimatorController _runtimeAnimatorController;

        private void Awake(){
            if(_animtorContainer == null){
                Debug.LogError("AnimatorGraphContainer is null");
                enabled = false;
                return;
            }

            _animator = GetComponent<Animator>();
            StartCoroutine(Config());
        }

        private IEnumerator Config(){
            yield return null;
            _playableOutput = _animtorContainer.RegisterAnimator(_animator);
            yield return null;
            _animtorContainer.ChangeControllerPlayable(_runtimeAnimatorController, ref _controllerPlayable);
            yield return null;
            _playableOutput.SetSourcePlayable(_controllerPlayable);       
        }

        private void OnEnable(){
            if(_controllerPlayable.IsValid()){ 
                _controllerPlayable.Play();
            }
        }

        private void OnDisable(){
            if(_controllerPlayable.IsValid()){ 
                _controllerPlayable.Pause();
            }
        }

        public void ChangeController(RuntimeAnimatorController runtimeAnimatorController){
            this._runtimeAnimatorController = runtimeAnimatorController;
            _animtorContainer.ChangeControllerPlayable(runtimeAnimatorController, ref _controllerPlayable);
            _playableOutput.SetSourcePlayable(_controllerPlayable);
        }

        private IEnumerator EnableCoroutine(){
            if(1f/Time.deltaTime < 30f){
                yield return null;
            }
            _playableOutput.SetSourcePlayable(_controllerPlayable);
            //_animatorUpdateManager.ForceEvaluate();
        }
    }
}