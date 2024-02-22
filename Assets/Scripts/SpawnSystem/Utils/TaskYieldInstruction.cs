using System.Threading.Tasks;

namespace Project.SpawnSystem
{
    public class TaskYieldInstruction : UnityEngine.CustomYieldInstruction
    {
        public override bool keepWaiting => (m_task.IsCanceled || m_task.IsCompleted) == false;

        private Task m_task;

        public TaskYieldInstruction(Task task){
            m_task = task;
        }
    }

    public static class TaskYieldInstructionExtensions{
        public static TaskYieldInstruction AsTaskYield(this Task task){
            return new TaskYieldInstruction(task);  
        }
    }
}