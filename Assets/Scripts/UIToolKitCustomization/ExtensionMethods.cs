using UnityEngine.UIElements;

namespace Project.UIToolkitCustomization
{
    public static class UIToolkitExtensionMethods
    {
        public static VisualElement CreateChild(this VisualElement parent, params string[] classes){
            VisualElement child = new VisualElement();
            child.AddClasses(classes).AddTo(parent);
            return child;
        }

        public static TElement CreateChild<TElement>(this VisualElement parent, params string[] classes) where TElement : VisualElement, new(){
            TElement child = new TElement();
            child.AddClasses(classes).AddTo(parent);
            return child;
        }

        public static TElement AddClasses<TElement>(this TElement element, params string[] classes) where TElement : VisualElement{
            for(int i = 0; i < classes.Length; ++i){
                element.AddToClassList(classes[i]);
            }
            return element;
        }

        public static TElement AddTo<TElement>(this TElement element, VisualElement parent) where TElement : VisualElement{
            parent.Add(element);
            return element;
        }

        public static TElement WithManipulator<TElement>(this TElement element, IManipulator manipulator) where TElement : VisualElement
        {
            element.AddManipulator(manipulator);
            return element;
        }
    }
}