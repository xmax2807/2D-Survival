namespace MyInventory{

    public interface IInventoryRepository{
        UnityEngine.Sprite GetItemIcon(ushort iconId);
        string GetItemDescription(int itemId);
        string GetItemName(int itemId);
        InventoryItemDTO[] GetAllItems();
    }
}