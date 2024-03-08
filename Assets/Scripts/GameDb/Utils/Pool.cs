using System;
using System.Collections.Generic;

namespace Project.GameDb
{
    public class Pool<T>
    {
        readonly int _objectId;
        readonly Queue<T> _objects;
        readonly Func<int, T> _factory;

        public Pool(Func<int, T> factory){
            if(factory == null){
                throw new ArgumentNullException(nameof(factory));
            }
            _factory = factory;
            _objects = new Queue<T>();
        }

        public T Get(){
            if(_objects.Count > 0){
                return _objects.Dequeue();
            }
            else{
                return _factory(_objectId);
            }
        }

        public void Return(T obj){
            if(obj == null){
                //ignore
                return;
            }
            _objects.Enqueue(obj);
        }
    }
}