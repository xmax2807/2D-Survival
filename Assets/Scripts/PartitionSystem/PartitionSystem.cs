using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.PartitionSystem
{
    public class PartitionSystem : MonoBehaviour
    {
        [SerializeField] private ScriptableEventProvider m_eventProvider;
        [SerializeField] private ChunkStorage m_chunkStorage;
        readonly Queue<IEnumerator> loadProcessQueue = new();
        ITrackedTarget target;
        EndlessGridManager m_gridManager;

        void Awake(){
           StartCoroutine(Init());
        }

        IEnumerator Init(){
            enabled = false;
            yield return m_chunkStorage.Initialize();
            m_gridManager = new EndlessGridManager(m_chunkStorage, chunkSize: 10);
            m_gridManager.OnTrackedTargetChanged(target);
            enabled = true;

            StartCoroutine(LoadProcess());
        }

        IEnumerator LoadProcess(){
            while(true){
                if(loadProcessQueue.Count == 0){
                    yield return null;
                    continue;
                }

                yield return loadProcessQueue.Dequeue();
            }
        }

        void OnEnable(){
            m_eventProvider.OnTrackedTargetChanged += OnTrackedTargetChanged;
        }

        void OnDisable(){
            m_eventProvider.OnTrackedTargetChanged -= OnTrackedTargetChanged;
        }
        void OnTrackedTargetChanged(ITrackedTarget target){
            this.target = target;
            m_gridManager?.OnTrackedTargetChanged(target);
        }
        void FixedUpdate(){
            if(target == null){
                return;
            }
            //FOR now calculate distance between target and current chunk
            Vector2Int currentChunkPosition = m_gridManager.CurrentChunkPosition;
            Vector2Int targetChunkPosition = m_gridManager.GetCellPositionAtTarget(target.position);

            if(currentChunkPosition != targetChunkPosition){
                loadProcessQueue.Enqueue(m_gridManager.Update());
            }
        }
    }
}