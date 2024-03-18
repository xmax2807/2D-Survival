using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    public class SequenceManager : MonoBehaviour
    {
        //TODO: Add addressable to this configuration in the future
        [SerializeField] private SequenceConfiguration configurationSO;
        //Then build sequences

        private readonly GameStateMachine _gameStateMachine = new GameStateMachine();
        private List<IGameState> _gameStates;
        private IGameState _initialState;

        void Start(){
            configurationSO.BuildGameStates(ref _gameStates, ref _initialState);
            _gameStateMachine.ChangeState(_initialState);
        }

        void Update(){
            _gameStateMachine.Update();
        }
    }
}