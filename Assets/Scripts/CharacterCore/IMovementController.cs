using System;
using UnityEngine;

namespace Project.CharacterBehaviour
{
    public interface IMovementController
    {
        void Enable();
        void Disable();
        void ChangeDirection(Vector2 direction);
        void ChangeSpeed(float speed);
        void Update();
    }

    public class RigidBodyController2D : IMovementController
    {
        bool isEnabled;
        Rigidbody2D m_rigidbody;
        private Vector2 m_direction;
        private Vector2 refVelocity;
        private float smoothTime;
        private float m_speed;
        public RigidBodyController2D(Rigidbody2D rigidbody, float smoothTime = 0f){
            m_rigidbody = rigidbody;
            this.smoothTime = smoothTime;
        }
        public void ChangeDirection(Vector2 direction)
        {
            m_direction = direction;
        }

        public void Update(){
            if(!isEnabled) return;
            if(m_direction == Vector2.zero && m_rigidbody.velocity == Vector2.zero) return;

            if(smoothTime == 0f){
                m_rigidbody.velocity =  m_speed * m_direction;
            }
            else{
                m_rigidbody.velocity = Vector2.SmoothDamp(m_rigidbody.velocity, m_direction * m_speed, ref refVelocity, smoothTime);
            }
            // if(m_direction == Vector2.zero) return;
            // m_direction -= m_rigidbody.velocity * Time.deltaTime;
            // m_rigidbody.velocity = m_direction * m_speed;
            //m_rigidbody.MovePosition(m_speed * Time.deltaTime * m_direction + (Vector2)m_target.position);
        }

        public void ChangeSpeed(float speed)
        {
            m_speed = speed;
        }

        public void Enable()
        {
            if(isEnabled) return;
            isEnabled = true;
            m_rigidbody.WakeUp();
        }

        public void Disable()
        {
            if(!isEnabled) return;
            isEnabled = false;
            m_rigidbody.Sleep();
        }
    }
}