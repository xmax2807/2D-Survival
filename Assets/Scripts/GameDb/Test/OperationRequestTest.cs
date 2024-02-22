using System.Collections;
using UnityEngine;

namespace Project.GameDb
{
    public class OperationRequestTest : MonoBehaviour
    {
        OperationManager m_manager = OperationManager.Instance;
        [SerializeField] int testValue;

        IEnumerator Start(){
            var operation = m_manager.RequestOperation<int>(TaskTest(), () => testValue);
            for(int i = 0; i < 10; ++i){
                int currentIndex = i;
                operation.Completed += (op) => Debug.Log($"Result in {currentIndex}: {op.Result}");
            }
            yield return operation;
            Debug.Log(operation.Result);
        }

        IEnumerator TaskTest(){
            var waitFor = new WaitForSeconds(0.5f);
            for(int i = 0; i < 10; ++i){
                yield return waitFor;
            }

            Debug.Log("Done");
        }
    }
}