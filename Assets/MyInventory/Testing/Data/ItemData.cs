using JetBrains.Annotations;

namespace MyInventory.Testing
{

    public enum ItemType : byte
    {
        Good,
        Weapon,
        Accessory
    }

    [System.Flags]
    public enum InventoryType : ushort
    {
        None = 0,
        Usable = 1,
        Sellable = 1 << 1,
        Craftable = 1 << 2,
    }
    [System.Serializable]
    public class ItemData
    {
        public ushort Id;
        public ushort IconId;
        public ItemType Type;
        public InventoryType InventoryType;
        public uint SellValue;
    }

    [System.Serializable]
    public class ItemString{
        public ushort Id;
        public string StringValue;
    }

    [System.Serializable]
    public struct PlayerInventoryItem{
        public int ItemId;
        public int Count;

        public PlayerInventoryItem(int itemId, int count){
            ItemId = itemId;
            Count = count;
        }

        public void SetCount(int count) => Count = count;
    }

}