using System.Collections;

namespace Project.GameEventSystem.EventGraph
{
    public enum StateMachineStatus : byte
    {
        Done = 0,
        NotStarted = 1,
        Running = 2
    }
    public readonly struct StateMachineResult
    {
        public readonly StateMachineStatus Status;
        public readonly int ResultValue;

        public StateMachineResult(StateMachineStatus status, int resultValue)
        {
            Status = status;
            ResultValue = resultValue;
        }

        public static StateMachineResult Default => new(StateMachineStatus.NotStarted, 0);

        public StateMachineResult SetResult(int newVal)
        {
            return new StateMachineResult(Status, newVal);
        }
        public StateMachineResult SetStatus(StateMachineStatus newStatus)
        {
            return new StateMachineResult(newStatus, ResultValue);
        }
    }
    public class StateMachineTree : Tree<SMNode>
    {
        public System.Guid Id => Root.Id;

        public StateMachineTree(SMNode root) : base(root)
        {
        }

        public System.Collections.IEnumerator Execute(IStateMachineContext context)
        {
            while (CurrentNode != null)
            {
                yield return CurrentNode.Run(context);
                NextNode();
            }
        }
    }
}