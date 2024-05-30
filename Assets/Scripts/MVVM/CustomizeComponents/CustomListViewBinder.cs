using System.Collections.Generic;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.Input;
using Project.UIToolkit;
using UnityEngine.UIElements;

namespace Project.MVVM{
    public abstract class CustomListViewBinder<TListView> : CollectionBinder<TListView> where TListView : VisualElement, ICustomListView
    {
        protected override void BindCollection()
        {
            CollectionElement.makeItem = DataTemplate.Instantiate;
            CollectionElement.bindItem = CollectionBindItem;
            CollectionElement.unbindItem = CollectionUnbindItem;
            CollectionElement.itemsSource = Data;
            Notifier.CollectionChanged += OnCollectionChanged;  

            if(Command == null) return;

            if(Command is IRelayCommand<IEnumerable<int>> indiceCommand)
            {
                CollectionElement.selectedIndicesChanged += indiceCommand.Execute;
            }
            else if(Command is IRelayCommand<IEnumerable<object>> selectionCommand){
                CollectionElement.selectionChanged += selectionCommand.Execute;
            }
        }

        protected override void UnbindCollection()
        {
            CollectionElement.itemsSource = null;
            CollectionElement.makeItem = null;
            CollectionElement.bindItem = null;
            CollectionElement.unbindItem = null;
            Notifier.CollectionChanged -= OnCollectionChanged;

            if (Command == null) return;
            if(Command is IRelayCommand<IEnumerable<int>> indiceCommand)
            {
                CollectionElement.selectedIndicesChanged -= indiceCommand.Execute;
            }
            else if(Command is IRelayCommand<IEnumerable<object>> selectionCommand){
                CollectionElement.selectionChanged -= selectionCommand.Execute;
            }
        }

        
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action is NotifyCollectionChangedAction.Add)
            {
                CollectionElement.RefreshItems();
                // Collection.RefreshItem(e.NewStartingIndex);
            }
            else if (e.Action is NotifyCollectionChangedAction.Remove)
            {
                CollectionElement.RefreshItems();
                // Collection.RefreshItem(e.OldStartingIndex);
            }
            else if (e.Action is NotifyCollectionChangedAction.Move)
            {
                CollectionElement.RefreshItem(e.OldStartingIndex);
                CollectionElement.RefreshItem(e.NewStartingIndex);
            }
            else if (e.Action is NotifyCollectionChangedAction.Replace)
            {
                CollectionElement.RefreshItem(e.OldStartingIndex);
            }
            else if (e.Action is NotifyCollectionChangedAction.Reset)
            {
                CollectionElement.RefreshItems();
            }
        }
    }
}