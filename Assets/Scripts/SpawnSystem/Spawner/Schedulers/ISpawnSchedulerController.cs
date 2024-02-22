using System.Collections;

namespace Project.SpawnSystem
{
    public interface ISpawnSchedulerController
    {
        IEnumerator Spawn();
        IEnumerator Prepare();
        ICommandSelector Selector{get;}
        ISpawner Spawner{get;}
        IReadOnlySchedulerContext Context{get;}
    }
}