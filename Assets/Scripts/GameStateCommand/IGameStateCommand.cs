using System.Collections;

namespace Project.GameStateCommand
{
    public interface IGameStateCommand
    {
        bool Finished { get; }

        IEnumerator Execute();
    }

    public class TestCommand : IGameStateCommand
    {
        public bool Finished => true;

        public IGameStateCommand Clone(object[] _)
        {
            return new TestCommand();
        }

        public IEnumerator Execute()
        {
            UnityEngine.Debug.Log("TestCommand: Execute");
            yield return null;
        }
    }
}