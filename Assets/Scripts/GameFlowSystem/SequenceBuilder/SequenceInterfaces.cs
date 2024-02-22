using System.Collections.Generic;

namespace Project.GameFlowSystem
{
    public interface ISequenceBuildDirector{
        void BuildSequences(ref List<IGameState> gameStates, IGameStateFactory factory, ScriptableSequenceData data);
    }

    public interface IGameStateFactory{
        IGameState CreateGameState(string builderId, SequenceData data);
        IGameLink CreateLink(string builderId, SequenceLinkData link, IGameState nextState);
    }
}