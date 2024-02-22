using System.Collections.Generic;

namespace Project.SpawnSystem
{
    /// <summary>
    /// This class is a Selector to select command like a Queue (FIFO)
    /// </summary>
    public class QueueCommandSelector : ICommandSelector
    {
        private Queue<ISpawnSchedulerCommand> _queue;
        public ISpawnSchedulerCommand Next()
        {
            EnsureQueueInit();
            if(_queue.Count > 0){
                return _queue.Dequeue();
            }
            return null;
        }

        public void SetNextCommand(ISpawnSchedulerCommand command)
        {
            EnsureQueueInit();
            _queue.Enqueue(command);
        }

        private void EnsureQueueInit(){
            if(_queue == null){
                _queue = new Queue<ISpawnSchedulerCommand>();
            }
        }
    }
}