using System.Collections.Generic;
namespace Project.Pooling
{
    public interface IPool<T>
    {
        T Get();
        void Return(T obj);
    }

    public abstract class AutoExpandPool<T> : IPool<T>
    {
        private Queue<T> _objects;
        public AutoExpandPool(int capacity = 10){
            _objects = new Queue<T>(capacity);
        }
        public T Get()
        {
            if(_objects.Count == 0){
                return CreateNewObject();
            }
            return _objects.Dequeue();
        }

        public void Return(T obj)
        {
            if(obj == null){
                return;
            }

            _objects.Enqueue(obj);
        }

        protected abstract T CreateNewObject();
    }

    public class AutoExpandPoolNew<T> : AutoExpandPool<T> where T : new()
    {
        protected override T CreateNewObject()
        {
            return new T();
        }
    }
}