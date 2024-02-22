using System.Collections;
using UnityEngine;

namespace Project.SpawnSystem
{
    public enum SpawnSchedulerStatus : byte{
        InActive, // in queue but not active
        ShouldPrepare, // its time to prepare
        Ready, // ready to spawn
        Active, // during spawn
        Completed // spawn completed
    }
    public interface ISpawnScheduler
    {
        IEnumerator Schedule();
    }

    public class DefaultSpawnScheduler : ISpawnScheduler, ISpawnSchedulerController
    {
        public ICommandSelector Selector {get;private set;}

        public ISpawner Spawner => m_spawner;

        private SchedulerContext m_context;
        public IReadOnlySchedulerContext Context => m_context;
        private ISpawner m_spawner;

        public DefaultSpawnScheduler(ICommandSelector selector, ISpawner spawner){
            m_spawner = spawner;
            Selector = selector;
        }

        public IEnumerator Prepare()
        {
            yield return m_spawner.Prepare();
        }

        public IEnumerator Spawn()
        {
            yield return m_spawner.Spawn();
            m_context.TotalSpawned += m_spawner.Context.SpawnCount;
            m_context.ActiveSpawnedCount = (uint)m_spawner.ActiveCount;
        }

        public IEnumerator Schedule()
        {
            m_context ??= new SchedulerContext();
            m_context.StartTime = Time.time;
            
            var command = Selector.Next();
            while(command != null){
                yield return command.Execute(this);
                command = Selector.Next();
            }
        }
    }
}