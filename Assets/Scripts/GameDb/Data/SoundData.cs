using UnityEngine;

namespace Project.GameDb{
    [System.Serializable]
    public class SoundData{
        [field: SerializeField] public int SoundId {get; set;}
        [field: SerializeField] public AudioClip Clip {get; set;}
    }
}