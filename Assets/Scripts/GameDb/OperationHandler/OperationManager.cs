using System;
using System.Collections;
using System.Collections.Generic;
using Project.Utils;

namespace Project.GameDb
{
    internal class OperationManager
    {
        const int MAX_ATTEMPTS = 10;
        readonly Dictionary<int, OperationContext> m_contexts;
        readonly Pooling.AutoExpandPoolNew<OperationContext> m_contextPool;

        internal static OperationManager Instance { get; } = new OperationManager();

        private OperationManager(){
            m_contextPool = new Pooling.AutoExpandPoolNew<OperationContext>();
            m_contexts = new Dictionary<int, OperationContext>();
        }

        internal GameAsyncOperation RequestOperation(IEnumerator operation, Func<object> resultGetter){
            int id;
            int attempts = MAX_ATTEMPTS;
            do{
                id = Guid.NewGuid().GetHashCode();
                --attempts;
            }
            while(attempts > 0 && m_contexts.ContainsKey(id));
            
            if(attempts <= 0){
                throw new Exception("Can't create operation due to id duplication");
            }

            OperationContext context = m_contextPool.Get();
            context.Reset();

            context.Task = Coroutines.StartCoroutine(ExecuteOperation(operation, resultGetter, context.Complete));

            m_contexts.Add(id, context);

            return new GameAsyncOperation(id);
        }

        internal GameAsyncOperation<TResult> RequestOperation<TResult>(IEnumerator operation, Func<TResult> resultGetter){
            int id;
            int attempts = MAX_ATTEMPTS;
            do{
                id = Guid.NewGuid().GetHashCode();
                --attempts;
            }
            while(attempts > 0 && m_contexts.ContainsKey(id));
            
            if(attempts <= 0){
                throw new Exception("Can't create operation due to id duplication");
            }

            OperationContext context = m_contextPool.Get();
            context.Reset();

            context.Task = Coroutines.StartCoroutine(ExecuteOperation(operation, resultGetter, context.Complete));

            m_contexts.Add(id, context);

            return new GameAsyncOperation<TResult>(id);
        }

        internal static OperationContext GetContext(int id) => Instance.m_contexts[id];

        private IEnumerator ExecuteOperation<TObject>(IEnumerator operation, Func<TObject> resultGetter, Action<TObject, System.Exception> callback){
            Exception e = null;
            yield return TryExecuting(operation, (exception) => e = exception);

            if(e != null){
                callback?.Invoke(default, e);
            }
            else{
                callback?.Invoke(resultGetter.Invoke(), null);
            }
        }

        private IEnumerator TryExecuting(IEnumerator operation, Action<System.Exception> callback = null){
            while(true){
                object current;
                try{
                    if(!operation.MoveNext()){
                        break;
                    }
                    current = operation.Current;
                }
                catch(System.Exception e){
                    callback?.Invoke(e);
                    yield break;
                }

                yield return current;
            }

            callback?.Invoke(null);
        }
    }
}