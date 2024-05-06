#if UNITY_EDITOR
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventCommandStateNode : EventNode{

        public event Action<int, int> IndexChangeEvent;
        public event Action<System.Collections.Generic.IEnumerable<int>> AddItemEvent;
        private DropdownField _commandDropDown;
        public DropdownField CommandDropDown {
            get => _commandDropDown;
            set {
                if(_commandDropDown == value) return;
                
                if(_commandDropDown is not null){
                    Remove(_commandDropDown);
                }
                _commandDropDown = value;
                Insert(1,_commandDropDown);
            }
        }

        public EventCommandStateNode(){
            InitListView();
        }
        

        private ListView _propertyItemContainer;
        public Action<VisualElement, int> UnbindItem {
            get => _propertyItemContainer.unbindItem;
            set => _propertyItemContainer.unbindItem = value;
        }
        public Action<VisualElement, int> BindItem{
            get => _propertyItemContainer.bindItem;
            set => _propertyItemContainer.bindItem = value;
        }
        public Func<VisualElement> MakeItem {
            get => _propertyItemContainer.makeItem;
            set => _propertyItemContainer.makeItem = value;
        }

        public System.Collections.IList ItemsSource{
            get => _propertyItemContainer.itemsSource;
            set {
                _propertyItemContainer.itemsSource = value;
                _propertyItemContainer.Rebuild();
            }
        }

        private void InitListView()
        {
            _propertyItemContainer = new ListView
            {
                reorderable = true,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                showFoldoutHeader = true,
                headerTitle = "Commands",
                showAddRemoveFooter = false,
                reorderMode = ListViewReorderMode.Animated,
            };
            _propertyItemContainer.itemIndexChanged += (index1, index2) =>{
                IndexChangeEvent?.Invoke(index1, index2);
                RefreshItems();
            };
            _propertyItemContainer.itemsAdded += AddItemEvent;
            Insert(2, _propertyItemContainer);
        }

        internal void RefreshItems() {
            _propertyItemContainer.Rebuild();
            RefreshExpandedState();
        }
        public void RefreshItem(int index){
            _propertyItemContainer.RefreshItem(index);
        }

        internal void RebuildListView()
        {
            _propertyItemContainer.Rebuild();
            RefreshExpandedState();
        }
    }
}
#endif