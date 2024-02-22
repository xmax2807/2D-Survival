using UnityEngine;
using UnityEngine.Events;

namespace Project.Utils
{
    public class RaycastCallback : MonoBehaviour{
        #if UNITY_EDITOR
        [SerializeField] bool _log = false;
        #endif
        [SerializeField] UnityEvent<RaycastHit2D[], int> _onRaycastEvent;
        [SerializeField] Transform originTransform;
        [SerializeField] Vector3 offset;
        [SerializeField] LayerMask targetLayer;
        [SerializeField] int maxHits;
        [SerializeField] float distance;
        private RaycastHit2D[] hits;

        void Awake(){
            hits = new RaycastHit2D[maxHits];
        }

        private void FixedUpdate(){
            int resultCount = Physics2D.RaycastNonAlloc(originTransform.position + offset, Vector2.zero, hits, distance: this.distance, layerMask: targetLayer);
            
            #if UNITY_EDITOR
            if(_log){
                Debug.Log($"Found {resultCount} hits at {originTransform.position}");
            }
            #endif

            if(_onRaycastEvent != null){
                _onRaycastEvent.Invoke(hits, resultCount);
            }
        }
    }
}