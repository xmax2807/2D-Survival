using UnityEngine;
using System.Collections.Generic;
namespace MyInventory.Testing{

    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "MyInventory/PlayerInventory")]
    public class PlayerInventory : ScriptableObject{
        [SerializeField] List<PlayerInventoryItem> m_items;
        public List<PlayerInventoryItem> Items => m_items;
        public int Count => m_items.Count;
        public PlayerInventoryItem this[int index] => m_items[index];

        public void AddItem(int itemId, int count){
            m_items.Add(new PlayerInventoryItem(itemId, count));
        }
        public void StackItem(int index, int count){
            count = m_items[index].Count + count;
            m_items[index].SetCount(count);
        }
        public void RemoveItem(int index){
            m_items.RemoveAt(index);
        }
    }
}