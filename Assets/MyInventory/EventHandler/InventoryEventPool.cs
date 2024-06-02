using System;
using System.Collections.Generic;

namespace MyInventory{
    public static class InventoryEventPool<TEvent> where TEvent : BaseInventoryEventData<TEvent>, new(){
        private const int MAX_POOL_SIZE = 8;
        private static IList<TEvent> m_pool = new List<TEvent>(MAX_POOL_SIZE);
        internal static TEvent GetEvent()
        {
            int count = m_pool.Count;
            if(count == 0 || count == MAX_POOL_SIZE){
                return new TEvent();
            }

            TEvent obj = m_pool[count - 1];
            m_pool.RemoveAt(count - 1);
            return obj;
        }

        internal static void ReleaseEvent(TEvent obj)
        {
            if(obj == null || m_pool.Count >= MAX_POOL_SIZE){
                return;
            }
            obj.Init();
            m_pool.Add(obj);
        }
    } 
}