using System;
using Project.Manager;
using UnityEngine;

namespace Project.GameEventSystem.Test
{
    public class TestEventSubscriber : MonoBehaviour
    {
        // define action type compatible with Delegate type
        private Action m_gameStartCallback;
        private void OnEnable(){
            m_gameStartCallback ??= new Action(OnGameStart);
            IEventAPI api = GameManager.Instance.GetService<IEventAPI>();
            api.GameStartEvent.Subscribe(m_gameStartCallback);
        }

        private void OnDisable(){
            if(m_gameStartCallback == null) return;
            IEventAPI api = GameManager.Instance.GetService<IEventAPI>();
            api.GameStartEvent.Unsubscribe(m_gameStartCallback);
        }

        private void OnGameStart(){
            Debug.Log("Game Started");
        }
    }
}