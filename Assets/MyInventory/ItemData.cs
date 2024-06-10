using System;
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
        public readonly ushort IconId;

        public InventorySlotItem(int id, int count, ushort iconId){
            ItemID = id;
            Count = count;
            IconId = iconId;
        }
    }

    public class InventoryItemDetail{
        public int ItemID {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public Sprite Icon {get;set;}
        public int Count {get;set;}
    }

    /// <summary>
    /// this class is transfered from real storage to internal inventory system
    /// </summary>
    public class InventoryItemDTO : IEquatable<InventoryItemDTO>{
        public readonly int ItemID;
        public readonly int ItemCount;
        public readonly uint SellValue;
        public readonly ushort IconID;

        public InventoryItemDTO(int id, int count, uint sellValue, ushort iconId){
            ItemID = id;
            SellValue = sellValue;
            IconID = iconId;
            ItemCount = count;
        }

        public readonly static InventoryItemDTO NullItem = new(-1, 0, 0, 0);
        public static bool IsNull(InventoryItemDTO item) => item.ItemID == -1;

        public bool Equals(InventoryItemDTO other)
        {
            return ItemID == other.ItemID;
        }
    }
}