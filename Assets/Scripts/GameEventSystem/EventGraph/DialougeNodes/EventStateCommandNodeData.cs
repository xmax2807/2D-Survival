using System;

namespace Project.GameEventSystem.EventGraph
{
    public class EventStateCommandNodeData : EventNodeData<CommandPlaceholderNodeItem>
    {
        [UnityEngine.SerializeField] public bool markAsLabel;
        [UnityEngine.SerializeField] public string LabelName;
        public CommandPlaceholderNodeItem[] Items => m_items;
        // m_items: CommandPlaceholderNodeItem[]

        #if UNITY_EDITOR
        public int this[int index] => m_items[index].commandId;
        public void AddCommandHolderAt(int index, CommandPlaceholderNodeItem item){
            //ensure in range
            m_items ??= new CommandPlaceholderNodeItem[0];
            if(index < 0 || index > m_items.Length) return;
            if(index == m_items.Length){
                System.Array.Resize(ref m_items, m_items.Length + 1);
            }
            m_items[index] = item;
            InvokeItemsChanged();
        }

        internal void RemoveCommandHolderAt(int index)
        {
            if(m_items.Length == 1){
                m_items = new CommandPlaceholderNodeItem[0];
                InvokeItemsChanged();
                return;
            }
            if(index < 0 || index >= m_items.Length){
                throw new System.IndexOutOfRangeException();
            }
            // swap to replace
            var newItems = new CommandPlaceholderNodeItem[m_items.Length - 1]; 
            for(int i = 0; i < index; i++){
                newItems[i] = m_items[i];
            }
            for(int i = index + 1; i < m_items.Length; i++){
                newItems[i - 1] = m_items[i];
            }
            m_items = newItems;
            InvokeItemsChanged();
        }
#endif
    }
}