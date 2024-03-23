using System.Collections;
using Project.Manager;
using UnityEngine.SceneManagement;

namespace Project.GameStateCommand
{
    public class SaveGameCommand : IGameStateCommand
    {
        public bool Finished => throw new System.NotImplementedException();

        public IEnumerator Execute()
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class LoadSystemCommand : IGameStateCommand
    {
        public bool Finished {get; private set;} = false;

        public IEnumerator Execute()
        {
            if (!Finished)
            {
                yield return GameManager.Instance.EssentialSystemsAwaiter();
                Finished = true;
            }
        }
    }

    public sealed class LoadMainGamePlayScene : IGameStateCommand
    {
        readonly string m_sceneName;
        public bool Finished {get; private set;}
        public LoadMainGamePlayScene(string sceneName)
        {
            m_sceneName = sceneName;
        }
        public IEnumerator Execute() {
            yield return SceneManager.LoadSceneAsync(m_sceneName, LoadSceneMode.Additive);
            Finished = true;
        }
    }
}