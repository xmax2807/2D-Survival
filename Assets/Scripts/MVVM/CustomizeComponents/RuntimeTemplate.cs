using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Project.MVVM
{
    public class RuntimeTemplate : VisualElement
    {
        public readonly List<(VisualElement, IItemCollectionBinder)> bindings = new();

        public void AddBinding(VisualElement element, params IItemCollectionBinder[] binders)
        {
            for(int i = 0; i < binders.Length; ++i){
                bindings.Add((element, binders[i]));
            }
        }
    }
}