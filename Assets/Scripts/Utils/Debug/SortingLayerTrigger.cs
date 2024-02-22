using UnityEngine;
namespace Project.Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SortingLayerTrigger : MonoBehaviour{
        private SpriteRenderer _spriteRenderer;
        private int currentSortingLayerValue;

        void Awake(){
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate(){
            int value = SortingLayer.GetLayerValueFromID(_spriteRenderer.sortingLayerID);
            if(value != currentSortingLayerValue){
                SetSortingLayer(value);
            }
        }

        private void SetSortingLayer(int sortingLayer){
            _spriteRenderer.sortingLayerID = sortingLayer;
            Debug.Log("Set sorting layer to: " + sortingLayer);
        }
    }
}