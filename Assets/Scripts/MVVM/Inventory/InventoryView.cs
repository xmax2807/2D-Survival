using MVVMToolkit;
using MVVMToolkit.Binding;
using Project.UIToolkit;
using Project.UIToolkitCustomization;
using Project.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.MVVM.Inventory{
    public class InventoryView : BaseView{
        [SerializeField] VisualTreeAsset inventorySlotTemplate;
        [SerializeField] string listViewBindingSource;
        [SerializeField] string collectionBinderName;
        [SerializeField] private StyleSheet inventoryStyleSheet;
        #region class_names
        public const string container_class_name = "container";
        public const string inventory_class_name = "inventory";
        public const string inventory_frame_class_name = "inventory__frame";
        public const string inventory_header_class_name = "inventory__header";
        public const string slot_container_class_name = "slot__container";
        #endregion

        protected override VisualElement Instantiate(){
            VisualElement root = new()
            {
                pickingMode = PickingMode.Ignore,
                name = gameObject.name
            };
            root.styleSheets.Add(inventoryStyleSheet);
            root.AddToClassList(RootUssClassName);
            return root;
        }

        protected override void OnInit(){
            base.OnInit();

            VisualElement container = RootVisualElement.CreateChild(container_class_name);
            VisualElement inventoryBody = container.CreateChild(inventory_class_name);

            inventoryBody.CreateChild(inventory_frame_class_name); // frame

            // to do add text to label
            inventoryBody.CreateChild(inventory_header_class_name).CreateChild<Label>().text = "Inventory";// header

            var gridView = inventoryBody.CreateChild<GridView>(slot_container_class_name); // body

            // set view data key to grid view
            System.Text.StringBuilder sb =  StringBuilderPool.GetStringBuilder();
            sb.Append("{~").Append(listViewBindingSource).Append('}')
            .Append(' ').Append("{*").Append(collectionBinderName).Append('}');
            
            gridView.viewDataKey = sb.ToString();

            StringBuilderPool.ReleaseStringBuilder(sb);

            DataTemplate template = new(inventorySlotTemplate);
            inventoryBody.Add(template);
        }

        protected override BindingParser ResolveBinding(){
            return new MyCustomBindingParser(BindingContext, RootVisualElement, LocalizedStringTables, LocalizedAssetTables);
        }
    }
}