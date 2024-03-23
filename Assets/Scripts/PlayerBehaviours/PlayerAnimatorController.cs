using System.Collections.Generic;
using Project.InputHandler;
using Project.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.PlayerBehaviour
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorController : MonoBehaviour, IPlayerControlInputListener
    {
        [SerializeField] private string m_xDirection;
        [SerializeField] private string m_yDirection;
        private Dictionary<string, int> m_paramMap;
        private Animator _animator;
        void Awake(){
            _animator = GetComponent<Animator>();
            m_paramMap = new Dictionary<string, int>();

            foreach(AnimatorControllerParameter param in _animator.parameters){
                m_paramMap.Add(param.name, Animator.StringToHash(param.name));
            }
        }

        void OnEnable(){
            GameManager.Instance.InputHandler.RegisterPlayerControlListener(this);
        }
        void OnDisable(){
            if(GameManager.Instance == null || GameManager.Instance.InputHandler == null) return;
            GameManager.Instance.InputHandler.UnregisterPlayerControlListener(this);
        }
        public void OnMove(Vector2 direction, InputActionPhase _)
        {
            if(!m_paramMap.ContainsKey(m_xDirection) || !m_paramMap.ContainsKey(m_yDirection)){
                return;
            }
            _animator.SetFloat(m_paramMap[m_xDirection], direction.x);
            _animator.SetFloat(m_paramMap[m_yDirection], direction.y);
        }
    }
}