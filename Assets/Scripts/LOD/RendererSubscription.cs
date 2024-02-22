using UnityEngine;

namespace Project.LOD
{
    public interface IRendererRegisterer{
        void RegisterRenderer(Renderer renderer);
        void UnregisterRenderer(Renderer renderer);
    }


    /// <summary>
    /// Give abilities: events, publish event to centeralize component to listener 
    /// </summary>
    public interface ICentralizedRenderer{
        event System.Action<int> OnRendererCountChanged;
    }
}