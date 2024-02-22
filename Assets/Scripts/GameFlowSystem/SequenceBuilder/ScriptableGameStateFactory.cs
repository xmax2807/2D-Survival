using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(fileName = "GameStateFactory", menuName = "FlowSystem/GameStateFactory")]
    public class ScriptableGameStateFactory : ScriptableObject, IGameStateFactory
    {
        
        [SerializeField] ScriptableGameStateBuilder[] AvailableBuilders;
        [SerializeField] EventProvider eventProvider;
        [SerializeField] CommandProvider commandProvider;
        private Dictionary<string, IGameStateBuilder> cacheBuilders;
        void OnEnable()
        {
            Init();
        }
        void OnDisable()
        {
            cacheBuilders?.Clear();
            cacheBuilders = null;
        }

        private void Init()
        {
            cacheBuilders = new Dictionary<string, IGameStateBuilder>();

            for(int i = 0; i < AvailableBuilders.Length; ++i)
            {
                cacheBuilders.Add(AvailableBuilders[i].IdentificationName, AvailableBuilders[i]);
            }
        }

        public IGameState CreateGameState(string builderId, SequenceData data)
        {
            if(cacheBuilders == null){
                Init();
            }
            if(!cacheBuilders.ContainsKey(builderId)){
                throw new NotImplementedException($"Builder with id {builderId} not found");
            }
            return cacheBuilders[builderId].BuildState(data, commandProvider);
        }

        public IGameLink CreateLink(string builderId, SequenceLinkData linkData, IGameState nextState)
        {
            if(cacheBuilders == null){
                Init();
            }
            if(!cacheBuilders.ContainsKey(builderId)){
                throw new NotImplementedException($"Builder with id {builderId} not found");
            }
            return cacheBuilders[builderId].BuildLink(linkData, nextState, eventProvider);
        }
    }
}