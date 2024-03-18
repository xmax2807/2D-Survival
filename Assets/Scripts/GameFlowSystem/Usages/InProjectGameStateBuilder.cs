using System.Collections;

namespace Project.GameFlowSystem.InProject
{
    public abstract class GameStateBuilder : IGameStateBuilder{
        private readonly string m_identificationName;
        public string IdentificationName => m_identificationName;

        public GameStateBuilder(string identificationName){
            m_identificationName = identificationName;
        }

        public abstract IGameState BuildState(SequenceData data, CommandProvider commandProvider);
    }
    public class LaunchGameStateBuilder : GameStateBuilder
    {
        public LaunchGameStateBuilder(string id) : base(id) { }

        public override IGameState BuildState(SequenceData data, CommandProvider commandProvider)
        {
            IEnumerator task = commandProvider.GetCommand(data.commandTypes[0]).GetTask();
            for(int i = 1; i < data.commandTypes.Length; ++i){
               task = task.Then(commandProvider.GetCommand(data.commandTypes[i]).GetTask());
            }
            return new FakeDelayedGameState(task: task, estimatedTime: 10);
        }
    }
}