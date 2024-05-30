using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Project.MVVM
{
    public class RuntimeTemplate : VisualElement
    {
        public readonly List<(VisualElement, IItemCollectionBinder)> bindings;
        public RuntimeTemplate(){
            focusable = true;
            bindings = new List<(VisualElement, IItemCollectionBinder)>(capacity: 4);
        }

        public void AddBinding(VisualElement element, params IItemCollectionBinder[] binders)
        {
            for(int i = 0; i < binders.Length; ++i){
                bindings.Add((element, binders[i]));
            }
        }
    }
}