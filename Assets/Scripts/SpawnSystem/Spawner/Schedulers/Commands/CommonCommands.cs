using System.Collections;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class SpawnPrepareCommand : ISpawnSchedulerCommand
    {
        public IEnumerator Execute(ISpawnSchedulerController scheduler)
        {
            yield return scheduler.Prepare();
        }
    }

    public class SpawnCommand : ISpawnSchedulerCommand
    {
        public IEnumerator Execute(ISpawnSchedulerController scheduler)
        {
            yield return scheduler.Spawn();
        }
    }

    public class WaitCommand : ISpawnSchedulerCommand
    {
        private readonly ISpawnSchedulerCommand nextCommand;
        private YieldInstruction waitInstruction;
        public WaitCommand(float time, ISpawnSchedulerCommand nextCommand) {
            waitInstruction = new WaitForSeconds(time);
            this.nextCommand = nextCommand;
        }

        public WaitCommand(YieldInstruction customWait, ISpawnSchedulerCommand nextCommand) {
            waitInstruction = customWait;
            this.nextCommand = nextCommand;
        }
        public IEnumerator Execute(ISpawnSchedulerController scheduler)
        {
            yield return waitInstruction;
            if(nextCommand != null){
                scheduler.Selector.SetNextCommand(command: nextCommand);
            }
        }
    }
}