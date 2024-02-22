using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameDb
{
    internal class OperationContext
    {
        public object Result { get; private set; }
        public Exception Exception;
        public Coroutine Task;
        public OperationStatus Status { get; private set; }
        event Action CompletedEvent;
        private Dictionary<int, Action> _callbackCache;

        public void Complete<TResult>(TResult result, Exception exception){
            Result = result;
            Exception = exception;

            if(Exception != null){
                Status = OperationStatus.Failed;
            }
            else{
                Status = OperationStatus.Completed;
            }

            CompletedEvent?.Invoke();
            //marked as completed
            Task = null;
        }

        public void Reset(){
            _callbackCache?.Clear();
            Status = OperationStatus.InProgress;
            Result = null;
            Exception = null;
            Task = null;
            CompletedEvent = null;
        }

        public void RegisterCallback(int id, Action callback){
            _callbackCache ??= new Dictionary<int, Action>();
            _callbackCache[id] = callback;
            CompletedEvent += callback;
        }

        public void UnregisterCallback(int id){
            if(_callbackCache == null) return;

            if(_callbackCache.ContainsKey(id)){
                CompletedEvent -= _callbackCache[id];
                _callbackCache.Remove(id);
            }
        }
    }
}