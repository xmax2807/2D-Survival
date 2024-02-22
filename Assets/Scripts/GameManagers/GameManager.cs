using Project.GameEvent;
using UnityEngine;

namespace Project.Manager
{
    public partial class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                // game is not playing return null
                if(!Application.isPlaying) return null;

                if (_instance == null)
                {
                    AutoCreate();
                }
                return _instance;
            }
        }

        private static void AutoCreate()
        {
            new GameObject("GameManager").AddComponent<GameManager>();
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            if (_instance == this)
            {
                Destroy(_instance.m_visibleRendererStorage);
                _instance = null;

                Debug.Log("Quit Game");
            }
        }
    }
}
