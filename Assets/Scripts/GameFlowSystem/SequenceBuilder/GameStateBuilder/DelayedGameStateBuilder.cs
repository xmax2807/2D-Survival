using System;
using System.Collections;

namespace Project.GameFlowSystem
{
    public class DelayedGameStateBuilder : ScriptableGameStateBuilder
    {
        public override IGameState BuildState(SequenceData data, CommandProvider commandProvider){
            // TODO: based on the task command type, try to get command from other system (e.g. Game Command System)
            // then create the DelayedGameState with that command IEnumerator task
            IEnumerator task = commandProvider.GetCommand(data.commandTypes[0]).GetTask();

            for(int i = 1; i < data.commandTypes.Length; ++i){
                task = task.Then(commandProvider.GetCommand(data.commandTypes[i]).GetTask());
            }
            return new FakeDelayedGameState(task: task, estimatedTime: 10, onProgressCallback: null, onCompleteCallback: null);
        }
    }
}