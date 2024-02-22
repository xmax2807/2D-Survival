namespace Project.GameFlowSystem
{
    public interface IGameStateBuilder
    {
        IGameState BuildState(SequenceData data, CommandProvider commandProvider);
        IGameLink BuildLink(SequenceLinkData data, IGameState nextState, EventProvider eventProvider);
    }
}