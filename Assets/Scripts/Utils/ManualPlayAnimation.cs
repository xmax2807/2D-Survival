using UnityEngine;

[RequireComponent(typeof(Animation))]
public class ManualPlayAnimatino : MonoBehaviour
{
    void Start(){
        GetComponent<Animation>().Play();
    }
}
