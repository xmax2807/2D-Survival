using UnityEngine;
namespace MyInventory
{
    public struct InventoryItemDataPersist{
        public int ItemID {get;set;}
        public int Count {get;set;}
    }
    public class InventoryDataPersist{
        public int PlayerId {get;set;}
        public InventoryItemDataPersist[] Items {get;set;}
    }
    
    /// <summary>
    /// used in grid slot
    /// </summary>
    public class InventorySlotItem{
        public readonly int ItemID;
        public int Count {get;set;}
        public readonly int Icon;
    }

    public class InventoryItemDetail{
        public int ItemID {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public Sprite Icon {get;set;}
        public int Count {get;set;}
    }
}