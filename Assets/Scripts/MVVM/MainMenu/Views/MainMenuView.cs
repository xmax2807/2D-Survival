using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkit;
using MVVMToolkit.Binding;
using Project.UIToolkitCustomization;
using UnityEngine.UIElements;

namespace Project.MVVM
{
    public class MainMenuView : BaseView, IRecipient<OpenViewMessage>
    {
        [UnityEngine.SerializeField] VisualTreeAsset MenuItemTemplate; 
        MyCustomBindingParser m_bindingParser;
        private ListView m_menuContainer;
        protected override VisualElement Instantiate()
        {
            var root = new VisualElement(){
                name = this.name
            };
            root.AddToClassList(RootUssClassName);
            m_menuContainer = root.CreateChild<ListView>();
            m_menuContainer.viewDataKey = "{~Source} {*CollectionViewBinder}";

            DataTemplate itemMenuTemplate = new(MenuItemTemplate);
            root.Add(itemMenuTemplate);
            return root;
        }

        protected override BindingParser ResolveBinding(){
            if(m_bindingParser == null){
                m_bindingParser = 
                new MyCustomBindingParser(BindingContext, RootVisualElement, LocalizedStringTables, LocalizedAssetTables);
            }

            return m_bindingParser;
        } 
        public void Receive(OpenViewMessage message) => enabled = true;
    }
}