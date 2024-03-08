using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Project.Manager;

namespace Project.LOD
{
    [CreateAssetMenu(fileName = "VisibleRendererStorage", menuName = "LOD/VisibleRendererStorage")]
    public class VisibleRendererStorage : ScriptableObject, IRendererRegisterer, ICentralizedRenderer{
        readonly Dictionary<int, Renderer> m_renderers = new();

        public event Action<int> OnRendererCountChanged;
        private Queue<IEnumerator> m_commandQueue;

        public void RegisterRenderer(Renderer renderer)
        {
            if(renderer == null){
                return;
            }
            int id = renderer.GetInstanceID();
            if(m_renderers.ContainsKey(id)) return;
            m_renderers[id] = renderer;

            m_commandQueue??= GameManager.Instance.CoroutineCommandQueue;
            m_commandQueue.Enqueue(InvokeEvent());
        }

        public void UnregisterRenderer(Renderer renderer)
        {
            if(renderer == null){
                return;
            }
            int id = renderer.GetInstanceID();
            if(m_renderers.ContainsKey(id)){
                m_renderers.Remove(id);
            }
            OnRendererCountChanged?.Invoke(m_renderers.Count);

            m_commandQueue??= GameManager.Instance.CoroutineCommandQueue;
            m_commandQueue.Enqueue(InvokeEvent());
        }

        private IEnumerator InvokeEvent(){
            yield return new WaitForEndOfFrame();
            OnRendererCountChanged?.Invoke(m_renderers.Count);
        }
    }
}