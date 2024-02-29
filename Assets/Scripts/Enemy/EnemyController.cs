using UnityEngine;
using Project.CharacterBehaviour;
using System;

namespace Project.Enemy
{
    public class EnemyController : IAIBehaviour
    {
        private Transform transform;
        private Transform target;
        RigidBodyController2D m_movementController;
        readonly float _attackRange;

        private bool m_enabled;
        public bool enabled { get => m_enabled;
            set{
                if(m_enabled == value) return;
                m_enabled = value;
                if(m_enabled){
                    OnEnable();
                }
                else{
                    OnDisable();
                }
            }
        }

        public EnemyController(Transform transform, Rigidbody2D rigidbody2D, float speed, float attackRange){
            this.transform = transform;
            m_movementController = new RigidBodyController2D(rigidbody2D);
            m_movementController.ChangeSpeed(speed);
            m_enabled = true;

            _attackRange = attackRange;
        }

        public void Update(){
            // Move to target use Rigidbody
            // find direction between this and target
            if(target == null){
                return;
            }
            Vector3 direction = target.position - transform.position;
            if(direction.sqrMagnitude < _attackRange){
                m_movementController.Disable();
                return;
            }
            else{
                m_movementController.Enable();
            }
            m_movementController.ChangeDirection(direction.normalized);
            m_movementController.Update();
        }

        void OnEnable(){
            m_movementController.Enable();
        }

        void OnDisable(){
            m_movementController.Disable();
        }

        public void OnTargetDetected(Core target)
        {
            if(target == null){
                this.target = null;
                return;
            }
            this.target = target.transform;
        }
    }
}