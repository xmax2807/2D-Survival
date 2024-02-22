using System;
using System.Collections;
using System.Collections.Generic;
using Project.Utils;
using UnityEngine;

namespace Project.GameFlowSystem
{
    public abstract class AbstractGameState : IGameState
    {
        private readonly List<IGameLink> _links;

        public AbstractGameState(){
            _links = new List<IGameLink>();
        }
        public void AddLink(IGameLink link)
        {
            if(link == null) return;
            _links.Add(link);
        }
        public void RemoveLink(IGameLink link)
        {
            if(link == null) return;
            _links.Remove(link);
        }

        public void DisableAllLinks()
        {
            for(int i = _links.Count - 1; i >= 0; --i){
                _links[i].Disable();
            }
        }

        public void EnableAllLinks()
        {
            for(int i = _links.Count - 1; i >= 0; --i){
                _links[i].Enable();
            }
        }

        public virtual void Enter(){}

        public abstract IEnumerator Execute();

        public virtual void Exit(){}

        public bool TryGetNextState(out IGameState nextState)
        {
            for(int i = _links.Count - 1; i >= 0; --i){
                if(_links[i].ValidateTransition(out nextState)){
                    return true;
                }
            }

            nextState = null;
            return false;
        }
    }

    /// <summary>
    /// This base class will execute a specified Action just once when entering the state.
    /// </summary>
    public class GameState : AbstractGameState
    {
        private readonly Action _executeCallback;

        public GameState(Action executeCallback){
            _executeCallback = executeCallback;
        }
        public override IEnumerator Execute()
        {
            yield return null; // wait for the next frame
            _executeCallback?.Invoke();
        }
    }

    public class FakeDelayedGameState : AbstractGameState
    {
        private readonly IEnumerator _task;
        private readonly Action<float> _onProgressCallback;
        private readonly Action _onCompleteCallback;
        private readonly float _estimatedTime;
        public FakeDelayedGameState(IEnumerator task, float estimatedTime, Action<float> onProgressCallback = null, Action onCompleteCallback = null){
            _task = task;
            _estimatedTime = estimatedTime;
            _onProgressCallback = onProgressCallback;
            _onCompleteCallback = onCompleteCallback;
        }

        public override IEnumerator Execute()
        {
            Coroutine fakeProgress = null;

            if(_onProgressCallback != null){
                try{
                    fakeProgress = Coroutines.StartCoroutine(FakeProgress(0.1f, _estimatedTime));
                }
                catch{
                // ignored
                }
            }
            yield return _task;
            Coroutines.StopCoroutine(ref fakeProgress);
            _onProgressCallback?.Invoke(1);
        }

        private IEnumerator FakeProgress(float interval, float totalTime)
        {
            float startTime = Time.time;
            YieldInstruction waitForInterval = new WaitForSeconds(interval);
            float currentRate = 0;

            while(currentRate < 0.85f){
                yield return waitForInterval;
                currentRate = (Time.time - startTime) / totalTime;
                _onProgressCallback?.Invoke(currentRate);
            }
        }

        public override void Exit()
        {
            _onCompleteCallback?.Invoke();
        }
    }
}