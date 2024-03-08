using UnityEngine;
namespace Project.GameDb{
    [System.Serializable]
    public class ParticleEffectData{
        public int id;
        public ParticleSystem vfx;
    }

    [System.Serializable]
    public class AnimatorEffectData{
        public int id;
        public int stateId;
    }
}