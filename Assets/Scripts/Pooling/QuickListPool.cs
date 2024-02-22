using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Pooling
{
    public static class QuickListPool<T>{
        private static IPool<List<T>> _pool = new CustomCreationPool<List<T>>(createCallback: () => new List<T>());
        private static IPool<T[]> _poolEmptyArray = new CustomCreationPool<T[]>(createCallback: () => Array.Empty<T>());
        public static List<T> GetList() => _pool.Get();
        public static void ReturnList(List<T> list) {
            if(list == null){
                return;
            }
            list.Clear();
            _pool.Return(list);
        }
        public static T[] GetEmptyArray() => _poolEmptyArray.Get();
        public static void ReturnEmptyArray(T[] array){
            if(array == null){
                return;
            }

            _poolEmptyArray.Return(array);
        }
    }
}