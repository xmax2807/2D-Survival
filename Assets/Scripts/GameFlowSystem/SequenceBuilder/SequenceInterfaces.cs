using System.Collections.Generic;

namespace Project.GameFlowSystem
{
    public interface ISequenceBuildDirector{
        void BuildSequences(ref List<IGameState> gameStates, IGameStateFactory factory, ScriptableSequenceData data);
    }

    public interface IGameStateFactory{
        IGameStateBuilder Resolve(string builderId);
    }
}