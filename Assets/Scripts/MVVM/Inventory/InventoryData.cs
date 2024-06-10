using UnityEngine;
namespace Project.MVVM.Inventory
{
    public class SlotData{
        public int ItemId;
        public int Quantity;
        public Sprite Icon;
        public Sprite Background;

        public SlotData(int itemId, int quantity, Sprite icon, Sprite background){
            ItemId = itemId;
            Quantity = quantity;
            Icon = icon;
            Background = background;
        }

        public SlotData(int itemId){
            ItemId = itemId;
            Quantity = 0;
            Icon = null;
            Background = null;
        }
    }
}