using System.Collections;

namespace Project.GameStateCommand
{
    public interface IGameStateCommand
    {
        bool Finished { get; }
        IEnumerator Execute();
    }
}