using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
#endif

namespace Project.GameFlowSystem
{
    public abstract class AbstractGameStateFactory : ScriptableObject, IGameStateFactory
    {
        protected Dictionary<string, IGameStateBuilder> cacheBuilders;

        protected abstract void InitBuilders();
        public IGameStateBuilder Resolve(string builderId)
        {
            if(cacheBuilders == null){
                InitBuilders();
            }
            if(!cacheBuilders.ContainsKey(builderId)){
                throw new NotImplementedException($"Builder with id {builderId} not found");
            }
            return cacheBuilders[builderId];
        }

        void OnDisable()
        {
            cacheBuilders?.Clear();
            cacheBuilders = null;

            #if UNITY_EDITOR
            NamesChangedEvent = null;
            #endif
        }

        #if UNITY_EDITOR
        public event Action<string[]> NamesChangedEvent;
        public string[] Names { get; protected set; }
        void OnValidate(){
            if(cacheBuilders == null){
                InitBuilders();
            }
            Names = cacheBuilders.Keys.ToArray();
            NamesChangedEvent?.Invoke(Names);
        }
        #endif
    }
}