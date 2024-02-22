using System;
using System.Collections.Generic;

namespace Project.Pooling
{
    public class CustomCreationPool<T> : AutoExpandPool<T>
    {
        private Func<T> _createCallback;
        public CustomCreationPool(Func<T> createCallback, int capacity = 0) : base(capacity)
        {
            _createCallback = createCallback ?? throw new ArgumentNullException("Pool creation callback cannot be null");
        }

        protected override T CreateNewObject()
        {
            return _createCallback.Invoke();
        }
    }
}