using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVMToolkit;
using MVVMToolkit.Binding.Custom;
using UnityEngine.UIElements;

namespace Project.MVVM
{
    public partial class MainMenuViewModel : ViewModel
    {
        [ObservableProperty] ObservableCollection<MenuButtonData> source;
        [RelayCommand]
        private void ButtonClicked(int index){
            // TODO: implement this

            UnityEngine.Debug.Log("Button clicked: " + index);
        }

        protected override void OnInit()
        {
            Source = new ObservableCollection<MenuButtonData>();
        }

        public void AddString(){
            int count = Source.Count;
            Source.Add(new MenuButtonData(clickCallback: ()=>ButtonClicked(count)){text = "New String"});
        }

        private void OnSelectedIndicesChanged(IEnumerable<int> indices){
            foreach (int index in indices)
            {
                UnityEngine.Debug.Log($"Selected index {index}");
            }
        }

        // This class is a custom binder that serves purpose to subscribe to `selectionChanged` event on ListView
        // Name of class is important as type name declared in UXML must match it - {*CollectionViewBinder}
        [UnityEngine.Scripting.Preserve]
        private class CollectionViewBinder : CustomBinder<ListView, MainMenuViewModel>
        {
            protected override void BindElement(){
                Element.selectedIndicesChanged += BindingContext.OnSelectedIndicesChanged;
            }

            protected override void UnbindElement() {
                Element.selectedIndicesChanged -= BindingContext.OnSelectedIndicesChanged;
            }
        }

        [Serializable]
        public class MenuButtonData{
            public string text;
            private readonly Action clickCallback;

            public MenuButtonData(Action clickCallback = null){
                this.clickCallback = clickCallback;
            }

            private class MenuButtonBinder : ItemCollectionBinder<MenuButtonData, Button>
            {
                protected override void Bind(MenuButtonData item, Button element)
                {
                    element.text = item.text;
                    if(item.clickCallback != null){
                        element.clicked += item.clickCallback;
                    }
                }

                protected override void Unbind(MenuButtonData item, Button element)
                {
                    element.text = item.text;
                    if(item.clickCallback != null){
                        element.clicked -= item.clickCallback;
                    }
                }
            }
        }
    }
}