using UnityEngine;
using UnityEngine.Events;
namespace Project.Utils
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderTriggerCallback : MonoBehaviour{

        [SerializeField, TagSelector] string specificTag;
        [SerializeField] UnityEvent<Collider2D> onTriggerEnter;
        [SerializeField] UnityEvent<Collider2D> onTriggerExit;


        void OnTriggerEnter2D(Collider2D other){
            if(true == other.CompareTag(specificTag)){
                onTriggerEnter.Invoke(other);
            }
        }

        void OnTriggerExit2D(Collider2D other){
            if(true == other.CompareTag(specificTag)){
                onTriggerExit.Invoke(other);
            }
        }
    }
}