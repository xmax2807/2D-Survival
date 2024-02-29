using UnityEngine;

namespace Project.GameEventSystem
{
    [System.Serializable]
    public struct SoundEventData
    {
        public int SoundId;
        public float Volume;

        public SoundEventData(int soundId, float volume){
            SoundId = soundId;
            Volume = volume;
        }
    }

    [System.Serializable]
    public class VisualEffectEventData 
    {
        public readonly int EffectId;
        public readonly Transform Target;

        public VisualEffectEventData(int effectId, Transform target){
            EffectId = effectId;
            Target = target;
        }
    }
}