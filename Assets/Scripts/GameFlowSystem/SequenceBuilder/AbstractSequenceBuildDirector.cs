using System.Collections.Generic;
using UnityEngine;
namespace Project.GameFlowSystem
{
    public abstract class AbstractSequenceBuildDirector : ScriptableObject, ISequenceBuildDirector{
        public IGameState DefaultState {get; protected set;} // init state
        protected Dictionary<int, IGameState> m_cache = new Dictionary<int, IGameState>();
        
        public abstract void BuildSequences(ref List<IGameState> gameStates, IGameStateFactory factory, ScriptableSequenceData data);
        protected IGameState RecursivelyCreateState(IGameStateFactory factory, ScriptableSequenceData data){

            //Create single state
            IGameState gameState;
            if (m_cache.ContainsKey(data.Id)){
                gameState = m_cache[data.Id];
            }
            else{
                gameState = factory.CreateGameState(data.BuilderId, data.SequenceData);
                m_cache.Add(data.Id, gameState);
            }

            // if there is no link, return
            if(data.Links == null || data.Links.Length == 0){
                return gameState;
            }
            
            //Create links
            for(int i = data.Links.Length - 1; i >= 0; --i){
                var sequenceData = data.Links[i].SequenceData;
                IGameState linkState;

                if(m_cache.ContainsKey(sequenceData.Id)){
                    linkState = m_cache[sequenceData.Id];
                }
                else{
                    linkState = RecursivelyCreateState(factory, sequenceData);
                    m_cache.Add(sequenceData.Id, linkState);
                }

                IGameLink link = factory.CreateLink(sequenceData.BuilderId, link: data.Links[i].Data, nextState: linkState);
                gameState.AddLink(link);
            }

            return gameState;
        }

        void OnDisable(){
            m_cache?.Clear();
        }
        void OnEnable(){
            m_cache ??= new Dictionary<int, IGameState>();
        }
    }
}