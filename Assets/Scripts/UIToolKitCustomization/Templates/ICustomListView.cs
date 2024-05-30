using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Project.UIToolkit
{
    public interface ICustomListView
    {
        IList itemsSource {get;set;}
        Func<VisualElement> makeItem {get;set;}
        Action<VisualElement, int> bindItem{get;set;}
        Action<VisualElement, int> unbindItem{get;set;}
        event Action<IEnumerable<int>> selectedIndicesChanged;
        event Action<IEnumerable<object>> selectionChanged;

        void RefreshItem(int index);

        /// <summary>
        /// refresh only unbind then bind items
        /// </summary>
        void RefreshItems();
        void Rebuild();
    }
}