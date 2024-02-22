using System.Collections;
using UnityEngine;
namespace Project.SpawnSystem
{
    public class AnimatorUpdateManager : MonoBehaviour
    {
        [SerializeField] AnimatorGraphContainer m_graphContainer;
        [SerializeField] private float[] updateInvervals; 
        public event System.Action<float> OnOverrideUpdate;
        private float _overrideDeltaTime;
        private YieldInstruction waitForOverrideUpdate;
        private Coroutine _overrideUpdateTask;

        void OnEnable()
        {
            if (_overrideUpdateTask != null)
            {
                StopCoroutine(OverrideUpdate());
            }
            _overrideUpdateTask = StartCoroutine(OverrideUpdate());
        }

        void OnDisable()
        {
            if (_overrideUpdateTask != null)
            {
                StopCoroutine(OverrideUpdate());
            }
        }

        IEnumerator OverrideUpdate()
        {
            OnDensityLODChanged(0);
            while (true)
            {
                if(m_graphContainer.PlayableGraph.IsValid() == false){
                    yield return new WaitUntil(() => m_graphContainer.PlayableGraph.IsValid());
                }
                m_graphContainer.PlayableGraph.Evaluate(_overrideDeltaTime);
                yield return waitForOverrideUpdate;
            }
        }

        public void OnDensityLODChanged(int LOD){
            LOD = Mathf.Min(LOD, updateInvervals.Length - 1);
            _overrideDeltaTime = updateInvervals[LOD];
            waitForOverrideUpdate = new WaitForSeconds(_overrideDeltaTime);
        }

        public void ForceEvaluate(){
            m_graphContainer.PlayableGraph.Evaluate(_overrideDeltaTime);
        }
    }
}