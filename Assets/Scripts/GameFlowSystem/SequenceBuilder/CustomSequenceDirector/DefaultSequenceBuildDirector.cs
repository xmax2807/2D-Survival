using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(menuName = "FlowSystem/DefaultSequenceDirector", fileName = "CustomSequenceDirector")]
    public class DefaultSequenceDirector : AbstractSequenceBuildDirector
    {
        //Assume gameStates result is not null
        public override void BuildSequences(ref List<IGameState> gameStates, IGameStateFactory factory, ScriptableSequenceData data)
        {
            m_cache.Clear();

            var firstState = RecursivelyCreateState(factory, data);
            

            DefaultState = firstState;

            gameStates.Clear();
            gameStates.AddRange(m_cache.Values);
        }

        
    }
}