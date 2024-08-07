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
            obj.transform.SetParent(container);
            return source;
        }

        public override AudioSource Get()
        {
            AudioSource source = base.Get();
            source.gameObject.SetActive(true);
            return source;
        }

        public override void Return(AudioSource source){
            if(source == null) return;
            base.Return(source);
            
            source.clip = null;
            source.gameObject.SetActive(false);
        }
    }
}