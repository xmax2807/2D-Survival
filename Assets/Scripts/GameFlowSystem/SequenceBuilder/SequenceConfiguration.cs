using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(fileName = "SequenceConfiguration", menuName = "FlowSystem/SequenceConfiguration", order = 1)]
    public class SequenceConfiguration : ScriptableObject
    {
        [SerializeField] private ScriptableGameStateFactory factory;
        [SerializeField] private AbstractSequenceBuildDirector director;
        
        //TODO, when Addressable imported, try to convert head sequence as Addressable for future expansion 
        [SerializeField] private ScriptableSequenceData headSequenceData;

        public void BuildGameStates(ref List<IGameState> gameStates, ref IGameState InitialState)
        {
            if(gameStates == null){
                gameStates = new List<IGameState>();
            }
            else{
                gameStates.Clear();
            }
            director.BuildSequences(ref gameStates, factory, headSequenceData);
            InitialState = director.DefaultState;
        }
    }
}