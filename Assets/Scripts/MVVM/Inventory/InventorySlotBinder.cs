namespace Project.MVVM.Inventory
{
    public class InventorySlotBinder : ItemCollectionBinder<SlotData, SlotElement>
    {
        protected override void Bind(SlotData item, SlotElement element)
        {
            element.SetIcon(item.Icon);
            element.SetStack(item.Quantity);
        }

        protected override void Unbind(SlotData item, SlotElement element)
        {
            element.SetIcon(null);
            element.SetStack(0);
        }
    }
}