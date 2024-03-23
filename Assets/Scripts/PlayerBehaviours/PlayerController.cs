using Project.InputHandler;
using Project.Manager;
using UnityEngine;
using Project.CharacterBehaviour;

namespace Project.PlayerBehaviour{
    /// <summary>
    /// This class includes movement controller, input controller
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlayerControlInputListener
    {
        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private float m_speed = 10f;
        [SerializeField] private float smoothTime = 0.1f;
        private IMovementController m_movementController;
        void Awake(){
            m_movementController = new RigidBodyController2D(m_rb, smoothTime: this.smoothTime);
            m_movementController.ChangeSpeed(m_speed);
        }

        void OnEnable(){
            GameManager.Instance.InputHandler.RegisterPlayerControlListener(this);
            m_movementController.Enable();
        }

        void OnDisable(){
            if(GameManager.Instance == null || GameManager.Instance.InputHandler == null) return;
            GameManager.Instance.InputHandler.UnregisterPlayerControlListener(this);
            m_movementController.Disable();
        }

        public void OnMove(Vector2 direction, UnityEngine.InputSystem.InputActionPhase _)
        {
            m_movementController.ChangeDirection(direction);
        }

        public void Update(){
            m_movementController.Update();
        }
    }
}