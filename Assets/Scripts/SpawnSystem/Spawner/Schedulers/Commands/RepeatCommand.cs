using System;
using System.Collections;

namespace Project.SpawnSystem
{
    public class RepeatCommand : ISpawnSchedulerCommand
    {
        private readonly ISpawnSchedulerCommand prepareCommand;
        private readonly ISpawnSchedulerCommand spawnCommand;
        private readonly ISpawnSchedulerCommand waitCommand;

        public RepeatCommand(ISpawnSchedulerCommand prepareCommand, ISpawnSchedulerCommand spawnCommand, ISpawnSchedulerCommand waitCommand){
            this.prepareCommand = prepareCommand;
            this.spawnCommand = spawnCommand;
            this.waitCommand = waitCommand;
        }

        public IEnumerator Execute(ISpawnSchedulerController scheduler)
        {
            yield return prepareCommand.Execute(scheduler);
            yield return spawnCommand.Execute(scheduler);
            yield return waitCommand.Execute(scheduler);
            scheduler.Selector.SetNextCommand(command: this);
        }
    }

    
}