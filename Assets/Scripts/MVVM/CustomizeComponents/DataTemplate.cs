using System;
using System.Collections.Generic;
using System.Linq;
using MVVMToolkit.Binding;
using UnityEngine.UIElements;

namespace Project.MVVM
{
    public class DataTemplate : VisualElement
    {
        readonly VisualTreeAsset _template;
        public DataTemplate(VisualTreeAsset template)
        {
            if(template == null){
                throw new ArgumentNullException(nameof(template));
            }
            _template = template;
            _binderMap = new Dictionary<int, IItemCollectionBinder[]>();

            style.display = new(DisplayStyle.None);
            Init();
        }

        private List<ElementData> _list;

        public VisualElement Instantiate()
        {
            if (_list is null)
            {
                Init();
            }

            var root = new RuntimeTemplate();
            var container = _template.Instantiate();
            root.Add(container);

            int i = 0;
            foreach (var ele in container.Children())
            {
                if (_binderMap.TryGetValue(i, out var binders))
                {
                    root.AddBinding(ele, binders);
                }

                ++i;
            }


            return root;
        }

        private class ElementData
        {
            public readonly int parentKey;
            public readonly Func<VisualElement> element;

            public ElementData(int parentKey, Func<VisualElement> element)
            {
                this.parentKey = parentKey;
                this.element = element;
            }
        }

        private readonly Dictionary<int, IItemCollectionBinder[]> _binderMap;

        private void Init()
        {
            VisualElement templateEle = _template.Instantiate();

            _binderMap.Clear();

            int childIndex = 0;
            foreach(VisualElement child in templateEle.Children()){
                var keys = ParsingUtility.GetFormatKeys(child.viewDataKey);
                if (keys is not null)
                {
                    IItemCollectionBinder[] binders = new IItemCollectionBinder[keys.Length];
                    _binderMap.Add(childIndex, binders);
                    for(int i = 0; i < keys.Length; ++i){
                        string key = keys[i];
                        if (key.StartsWith(':'))
                        {
                            binders[i] = ItemCollectionMap.GetInstance(key[1..]);
                        }
                    }
                }
                ++childIndex;
            }
            Clear();
        }
    }
}