using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MVVMToolkit;
using MVVMToolkit.Binding.Custom;
using Project.UIToolkit;

namespace Project.MVVM.Inventory
{
    public partial class InventoryViewModel : ViewModel
    {
        [ObservableProperty]
        ObservableCollection<SlotData> _items;
        [UnityEngine.SerializeField] UnityEngine.Sprite icon;
        public void OnSelectedIndicesChanged(IEnumerable<int> indices){
            foreach (int index in indices)
            {
                UnityEngine.Debug.Log($"Selected index {index}");
            }
        }

        public void OnSelectedObjectChanged(IEnumerable<object> objects){
            foreach (object obj in objects)
            {
                UnityEngine.Debug.Log($"Selected {obj}");
            }
        }
        public void OnItemsChosen(IEnumerable<object> objects){
            foreach (object obj in objects)
            {
                UnityEngine.Debug.Log($"Chosen {obj}");
            }
        }

        protected override void OnInit(){
            Items = new ObservableCollection<SlotData>();
            for(int i = 0; i < 100; ++i){
                Items.Add(new SlotData(i){
                    Quantity = 10,
                    Icon = icon,
                });
            }
        }

        [UnityEngine.Scripting.Preserve]
        private class InventoryBinder : CustomBinder<GridView, InventoryViewModel>
        {
            protected override void BindElement(){
                Element.selectedIndicesChanged += BindingContext.OnSelectedIndicesChanged;
                Element.selectionChanged += BindingContext.OnSelectedObjectChanged;
                Element.onItemsChosen += BindingContext.OnItemsChosen;
            }

            protected override void UnbindElement() {
                Element.selectedIndicesChanged -= BindingContext.OnSelectedIndicesChanged;
                Element.selectionChanged -= BindingContext.OnSelectedObjectChanged;
                Element.onItemsChosen -= BindingContext.OnItemsChosen;
            }
        }
    }
}