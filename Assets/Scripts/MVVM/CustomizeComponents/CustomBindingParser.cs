using System.ComponentModel;
using MVVMToolkit.Binding;
using MVVMToolkit.Binding.Localization;
using UnityEngine.UIElements;
using MVVMToolkit.Binding.Tooltips;
using UnityEngine;
using UnityEngine.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MVVMToolkit.Binding.Custom;

namespace Project.MVVM{
    public class MyCustomBindingParser : BindingParser
    {
        public MyCustomBindingParser(INotifyPropertyChanged bindingContext, VisualElement root,
            LocalizedStringTable[] stringTables,
            LocalizedAssetTable[] assetTables) : base(bindingContext, root, stringTables, assetTables)
        {
            ViewDataKeyStores.Add(new CollectionParser(Binding));
            RefreshParseBinding(root);
        }

        public void RefreshParseBinding(VisualElement item){
            ParseAll(item, item =>
            {
                Parse(item, TextStores, textGetter);
                Parse(item, TooltipStores, tooltipGetter);
                ParseMultiple(item, ViewDataKeyStores, viewDataKeyGetter);
            });
        }

        private static void ParseAll(VisualElement root, Action<VisualElement> funcCall)
        {
            funcCall(root);
            foreach (var element in root.Children())
            {
                ParseAll(element, funcCall);
            }
        }

        private void ParseMultiple<T>(VisualElement item, List<IBindingParser> stores, Func<T, string> keyGetter)
            where T : VisualElement
        {
            if (item is not T target)
            {
                return;
            }

            var key = keyGetter(target);
            if (string.IsNullOrEmpty(key)) return;

            var bindings = ParsingUtility.GetFormatKeys(key);

            if (bindings is null) return;

            foreach (var binding in bindings)
            {
                foreach (var store in stores)
                {
                    if (binding.StartsWith(store.Symbol()))
                    {
                        try
                        {
                            store.Process(target, binding[1..]);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Key that caused error: {binding}. Binding type: {Binding.GetType()}");
                            Debug.LogException(e);
                        }
                    }
                }
            }
        }


        private void Parse<T>(VisualElement item, List<IBindingParser> stores, Func<T, string> keyGetter)
            where T : VisualElement
        {
            if (item is not T target)
            {
                return;
            }

            var key = keyGetter(target);
            if (string.IsNullOrEmpty(key)) return;
            foreach (var store in stores)
            {
                if (key.StartsWith(store.Symbol()))
                {
                    try
                    {
                        store.Process(target, key[1..]);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Key that caused error: {key}. Binding type: {Binding.GetType()}");
                        Debug.LogException(e);
                    }
                }
            }
        }
    }
}