using System.Collections;

namespace Project.SpawnSystem
{
    public interface ISpawner
    {
        IEnumerator Prepare();
        IEnumerator Spawn();
        ISpawnContext Context { get; }
        int ActiveCount { get; }
    }
}