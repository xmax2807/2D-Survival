using Project.CharacterBehaviour;
using UnityEngine;

namespace Project.Enemy
{
    public class EnemyAICore : MonoCoreComponent, IAIBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D; // for movement
        [SerializeField] private float _speed;
        [SerializeField] private float _attackRange = 0.5f;
        IAIBehaviour m_movementController;
        private Core m_target;

        override protected void AfterAwake(){
            m_movementController = new EnemyController(transform, _rigidbody2D, _speed, _attackRange);
        }
        public void OnTargetDetected(Core target)
        {
            m_movementController.OnTargetDetected(target);
        }

        public void OnColliderDetected(Collider2D collider){
            if(collider.TryGetComponent<Core>(out Core target)){
                m_target = target;
                OnTargetDetected(m_target);
                enabled = true;
            }
        }

        public void OnColliderLost(Collider2D collider){
            if(collider.TryGetComponent<Core>(out Core target)){
                if(target == m_target){
                    enabled = false;
                    m_target = null;
                }
            }
        }

        void OnEnable(){
            m_movementController.enabled = true;
        }
        void OnDisable(){
            m_movementController.enabled = false;
        }

        public void Update()
        {
            m_movementController.Update();
        }
    }
}