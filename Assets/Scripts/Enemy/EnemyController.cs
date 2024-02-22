using UnityEngine;
using Project.CharacterBehaviour;
using System;

namespace Project.Enemy
{
    public class EnemyController : IAIBehaviour
    {
        private Transform transform;
        private Transform target;
        //private float _speed;
        RigidBodyController2D m_movementController;

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

        public EnemyController(Transform transform, Rigidbody2D rigidbody2D, float speed){
            this.transform = transform;
            m_movementController = new RigidBodyController2D(rigidbody2D);
            m_movementController.ChangeSpeed(speed);
            m_enabled = true;
        }

        public void Update(){
            // Move to target use Rigidbody
            // find direction between this and target
            if(target == null){
                return;
            }
            Vector3 direction = (target.position - transform.position).normalized;
            m_movementController.ChangeDirection(direction);
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