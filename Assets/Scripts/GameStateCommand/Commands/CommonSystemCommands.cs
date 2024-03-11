using System.Collections;

namespace Project.GameStateCommand
{
    public class SaveGameCommand : IGameStateCommand
    {
        public bool Finished => throw new System.NotImplementedException();

        public IGameStateCommand Clone(object[] _)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}