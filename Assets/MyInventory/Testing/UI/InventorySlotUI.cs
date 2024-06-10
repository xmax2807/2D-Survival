using UnityEngine;
using UnityEngine.UI;
namespace MyInventory.Testing{
    public class InventorySlotUI : Selectable{
        [SerializeField] private Image icon;
        [SerializeField] private Text amount;

        public void SetIcon(Sprite sprite){
            icon.sprite = sprite;
        }
        public void SetAmount(int amount){
            if(amount <= 0){
                this.amount.text = string.Empty;
                return;
            }
            this.amount.text = amount.ToString();
        }
    }
}