namespace MyInventory{

    public interface IInvetoryRepository{
        void SaveInventoryData(InventoryDataPersist data);
        InventoryDataPersist LoadInventoryData(int playerId);
        UnityEngine.Sprite GetItemIcon(int id);
        InventoryItemDetail GetItemDetail(int id);
    }
}