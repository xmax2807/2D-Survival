using System.Collections.Generic;
using UnityEngine;

namespace Project.VisualEffectSystem
{
    public class EffectPool<T> where T : Component
    {
        readonly Queue<T> _pool;
        readonly T _template;
        readonly Transform _container;
        public EffectPool(Transform container, T template)
        {
            _pool = new Queue<T>(capacity: 1);
            _template = template;
            _container = container;
        }


        public T Get(){

            T result;
            if(_pool.Count > 0){
                result = _pool.Dequeue();
            }
            else{
                result = Create();
            }

            result.gameObject.SetActive(true);

            return result;
        }

        public void Return(T component){
            if(component == null){
                return;
            }
            component.gameObject.SetActive(false);
            _pool.Enqueue(component);
        }

        private T Create(){
            T component = UnityEngine.Object.Instantiate<T>(_template, _container);
            component.gameObject.SetActive(false);
            return component;
        }
    }
}