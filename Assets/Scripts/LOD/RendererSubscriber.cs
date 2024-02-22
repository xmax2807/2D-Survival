using Project.Manager;
using UnityEngine;

namespace Project.LOD
{
    [RequireComponent(typeof(Renderer))]
    public class RendererSubscriber : MonoBehaviour{
        private Renderer _renderer;
        private IRendererRegisterer _registerer;
        void Awake(){
            _renderer = GetComponent<Renderer>();
        }
        void Start(){
            _registerer = GameManager.Instance.StorageRendererRegisterer;
        }
        void OnBecameVisible(){
            _registerer.RegisterRenderer(_renderer);
        }

        void OnBecameInvisible(){
            _registerer.UnregisterRenderer(_renderer);
        }
    }
}