using System;

namespace MyInventory
{
    public interface IInventoryEventListener
    {
        void OnReceive(int itemId, int count);
        void OnUse(int itemId, int count); // this can be sold, drop, used, etc.
        void OnPlayerRequestOpenInventory();
    }
}