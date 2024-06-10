using System;

namespace MyInventory.Testing
{
    internal class Pool<TPoolable>{
        private readonly System.Func<TPoolable> m_creator;
        private readonly int m_maxPoolSize;
        private readonly TPoolable[] m_pool;
        private int m_availableSlotCount;

        public Pool(System.Func<TPoolable> creator, int capacity){
            m_creator = creator ?? throw new ArgumentException("creator is null");
            m_maxPoolSize = capacity;
            m_pool = new TPoolable[capacity];
            for(int i = 0; i < capacity; ++i){
                m_pool[i] = creator();
            }

            m_availableSlotCount = 0;
        }
        public Pool(System.Func<TPoolable> creator) : this(creator, 16){}

        // return null if pool is full of rent objects
        public TPoolable Get(){
            if(m_availableSlotCount == 0){
                return default;
            }
            return m_pool[--m_availableSlotCount];
        }

        // if obj is null or pool is full of unrent objects, it will be ignored
        public void Return(TPoolable obj){
            if(obj != null && m_availableSlotCount < m_maxPoolSize){
                m_pool[m_availableSlotCount] = obj;
                ++m_availableSlotCount;
            }
        }
    }
}