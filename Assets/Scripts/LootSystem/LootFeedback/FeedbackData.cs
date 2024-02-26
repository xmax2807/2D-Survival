using UnityEngine;

namespace Project.LootSystem
{
    public enum FeedbackType : byte{
        None,
        PlaySound,
        PlayVFX,
        PopDetailUI, // for new item obtained
    }
    /// <summary>
    /// base class for feedback
    /// </summary>
    [System.Serializable]
    public class FeedbackData
    {
        [field: SerializeField] public FeedbackType Type {get;private set;}
        // for PlaySound
        [field: SerializeField] public int soundId {get;private set;}
        [field: SerializeField] public float volume {get;private set;}
        // for PlayVFX
        [field: SerializeField] public int vfxId {get;private set;}
        
    }
}