using System;
using UnityEngine;

namespace Project.AudioSystem
{
    public class AudioPool : Pooling.AutoExpandPool<AudioSource>
    {
        readonly Transform container;
        public AudioPool(Transform container, int capacity) : base(capacity)
        {
            this.container = container;
        }

        protected override AudioSource CreateNewObject()
        {
            GameObject obj = new GameObject("AudioSource");
            AudioSource source = obj.AddComponent<AudioSource>();
            return source;
        }
    }
}