using System;
using System.Collections;

namespace Project.GameDb
{
    public enum OperationStatus : byte
    {
        InProgress,
        Completed,
        Failed
    }
    public readonly struct GameAsyncOperation : IEnumerator
    {
        public OperationStatus Status => Context.Status;
        private OperationContext Context => OperationManager.GetContext(_id);
        private readonly int _id;

        internal GameAsyncOperation(int id){
            _id = id;
        }

        public bool IsDone => Status == OperationStatus.Completed || Status == OperationStatus.Failed;
        public object Result => Context.Result;
        public event Action<GameAsyncOperation> Completed {
            add => RegisterEvent(value);
            remove => UnregisterEvent(value);
        }

        private void UnregisterEvent(Action<GameAsyncOperation> value)
        {
            if(value == null) return;

            Context.UnregisterCallback(value.GetHashCode());
        }

        private void RegisterEvent(Action<GameAsyncOperation> value)
        {
            if(value == null) return;

            int hashCode = value.GetHashCode();
            int operationId = this._id;
            Context.RegisterCallback(hashCode, () => value(new GameAsyncOperation(operationId)));
        }


        public readonly GameAsyncOperation<T> As<T>(){
            if(typeof(object) is not T){
                throw new InvalidOperationException("Invalid cast");
            }

            return new GameAsyncOperation<T>(_id);
        }

        #region IEnumerator
        public readonly object Current => null; // can be yield instruction or IEnumerator
        public readonly bool MoveNext()
        {
            //return true to coninue loop next frame
            //return false if done
            return IsDone == false;
        }

        public readonly void Reset()
        {
            //empty for coroutine
        }
        #endregion
    }

    public readonly struct GameAsyncOperation<T> : IEnumerator{
        private OperationStatus Status => Context.Status;
        private OperationContext Context => OperationManager.GetContext(_id);
        private readonly int _id;

        internal GameAsyncOperation(int id){
            _id = id;
        }

        public bool IsDone {
            get{
                var _status = Status;
                return _status == OperationStatus.Completed || _status == OperationStatus.Failed;
            }
        }
        public T Result => (T)Context.Result;

        private void UnregisterEvent(Action<GameAsyncOperation<T>> value)
        {
            if(value == null) return;

            Context.UnregisterCallback(value.GetHashCode());
        }

        private void UnregisterEventTypeless(Action<GameAsyncOperation> value){
            if(value == null) return;

            Context.UnregisterCallback(value.GetHashCode());
        }

        private void RegisterEvent(Action<GameAsyncOperation<T>> value)
        {
            if(value == null) return;

            int hashCode = value.GetHashCode();
            int operationId = this._id;
            Context.RegisterCallback(hashCode, () => value(new GameAsyncOperation<T>(operationId)));
        }

        private void RegisterEventTypeless(Action<GameAsyncOperation> value){
            if(value == null) return;

            int hashCode = value.GetHashCode();
            int operationId = this._id;
            Context.RegisterCallback(hashCode, () => value(new GameAsyncOperation(operationId)));
        }

        public event Action<GameAsyncOperation> CompletedTypeless{
            add => RegisterEventTypeless(value);
            remove => UnregisterEventTypeless(value);
        }
        public event Action<GameAsyncOperation<T>> Completed{
            add => RegisterEvent(value);
            remove => UnregisterEvent(value);
        }
        public static explicit operator GameAsyncOperation(GameAsyncOperation<T> op) => new(op._id);

        #region IEnumerator
        public readonly object Current => null; // can be yield instruction or IEnumerator
        public readonly bool MoveNext()
        {
            //return true to coninue loop next frame
            //return false if done
            return this.IsDone == false;
        }

        public readonly void Reset()
        {
            //empty for coroutine
        }
        #endregion
    }
}