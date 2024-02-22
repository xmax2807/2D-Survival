using UnityEngine;

namespace Project.Utils
{
    public class DebugBoolTrigger : MonoBehaviour
    {
        public void OnBoolChanged(bool value){
            Debug.Log("OnBoolChanged: " + value);
        }
    }
}