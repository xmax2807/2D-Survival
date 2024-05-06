#if UNITY_EDITOR
using UnityEngine.UIElements;
using System;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.Experimental.GraphView.Port;
namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventConditionStateNode : EventNode{
        
        private DropdownField _conditionDropdown;
        public DropdownField ConditionDropdown{
            get => _conditionDropdown;
            set {
                if(_conditionDropdown == value) return;

                if(_conditionDropdown is not null){
                    Remove(_conditionDropdown);
                }
                _conditionDropdown = value;
                Insert(1, _conditionDropdown);
            }
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
        
        public event Action<System.Collections.Generic.IEnumerable<int>> AddItemEvent{
            add => this._propertyItemContainer.itemsAdded += value;
            remove => this._propertyItemContainer.itemsAdded -= value;
        }

        public event Action<int,int> IndexChangeEvent;

        public EventConditionStateNode(){
            InitListView();
            AddInputPort("Input");
        }

        public void ClearOutputPort(){
            this.outputContainer.Clear();
        }

        public void AddInputPort(string portName){
            Port port = CreatePort(Orientation.Horizontal, Direction.Input, Capacity.Single, typeof(float));
            port.portName = portName;
            this.inputContainer.Add(port);
            this.RefreshPorts();
        }

        public void AddOutputPort(string portName){
            Port port = CreatePort(Orientation.Horizontal, Direction.Output, Capacity.Single, typeof(float));
            port.portName = portName;
            this.outputContainer.Add(port);
            this.RefreshPorts();
        }

        public void ChangePortName(int index, string portName){
            this.outputContainer[index].Q<Port>().portName = portName;
        }

        private Port CreatePort(Orientation orientation, Direction direction, Capacity capacity, Type type){
            return this.InstantiatePort(orientation, direction, capacity, type);
        }

        private void InitListView()
        {
            _propertyItemContainer = new ListView
            {
                reorderable = true,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                showFoldoutHeader = true,
                headerTitle = "Conditions",
                showAddRemoveFooter = false,
                reorderMode = ListViewReorderMode.Animated,
            };
            _propertyItemContainer.itemIndexChanged += (index1, index2) =>{
                IndexChangeEvent?.Invoke(index1, index2);
                RefreshItems();
            };
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