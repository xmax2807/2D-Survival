using System.Collections;
using System.Collections.Generic;

namespace Project
{
    public struct ReadonlyArray<T> : IReadOnlyList<T>
    {
        private List<T> list;

        public ReadonlyArray(List<T> array) => list = array;

        public T this[int index] => list[index];

        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}