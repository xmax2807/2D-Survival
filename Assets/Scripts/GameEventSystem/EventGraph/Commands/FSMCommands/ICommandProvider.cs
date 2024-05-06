using System.Collections;

namespace Project.GameEventSystem.EventGraph
{
    public interface IFSMCommand {
        IEnumerator Execute(IStateMachineContext context);
    }
    public interface ICondition {
        bool IsConditionMet();
    }
    public interface ICommandProvider{
        IEnumerator WaitForCommand(int commandId, params int[] args);
        void ExecuteCommand(int commandId, params int[] args);
        int ExecuteCommandWithResult(int commandId, params int[] args);
    }
}