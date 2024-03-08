using UnityEngine;
namespace Project.GameEventSystem
{
    [System.Serializable]
    public readonly struct VisualEffectEventData 
    {
        public enum EffectType : byte{
            Particle, Animator
        }
        public readonly int EffectId;
        public readonly EffectType FXType;
        public readonly Vector2 Position;

        public VisualEffectEventData(int effectId, EffectType type, Vector2 position) {
            EffectId = effectId;
            FXType = type;
            Position = position;
        }
    }
}