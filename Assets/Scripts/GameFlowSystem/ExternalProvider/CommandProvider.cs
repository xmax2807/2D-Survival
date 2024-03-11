using System.Collections;

namespace Project.GameFlowSystem
{
    public interface IGameStateCommand{
        IEnumerator GetTask();
    }
    public abstract class CommandProvider : UnityEngine.ScriptableObject{
        public abstract IGameStateCommand GetCommand(CommandType commandType);
        public abstract IGameStateCommand GetCommand(CommandType commandType, params object[] args);
    }
}