using System;
using System.Collections;
using Project.Utils;
using UnityEngine;

namespace Project.GameFlowSystem
{
    public class GameStateMachine : IDisposable
    {
        public IGameState CurrentState { get; private set; }
        private Coroutine m_currentStateTask;

        public void Resume(){
            if(CurrentState == null){
                Debug.LogWarning("GameStateMachine: No state to resume");
                return;
            }

            CancelCurrentStateTask();
            Coroutines.StartCoroutine(Play());
        }

        public void ChangeState(IGameState newState)
        {

            if (newState == null) return;

            if (CurrentState != null)
            {
                CancelCurrentStateTask();
                CurrentState.Exit();
            }

            CurrentState = newState;
            Coroutines.StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            CurrentState.Enter();
            m_currentStateTask = Coroutines.StartCoroutine(CurrentState.Execute());
            yield return m_currentStateTask;
            m_currentStateTask = null;
        }

        private void CancelCurrentStateTask()
        {
            //no need to pre-check, method already check null before cancelling
            Coroutines.StopCoroutine(ref m_currentStateTask);
        }

        public void Update()
        {
            if (CurrentState != null && m_currentStateTask == null) //current state is done playing
            {
                if (CurrentState.TryGetNextState(out IGameState nextState))
                {
                    CurrentState.DisableAllLinks();
                    ChangeState(nextState);
                    CurrentState.EnableAllLinks();
                }
            }
        }

        public void Stop()
        {
            if (CurrentState != null && m_currentStateTask != null)
            {
                CancelCurrentStateTask();
            }
        }

        public void Dispose()
        {
            Stop();
            CurrentState = null;
        }
    }
}