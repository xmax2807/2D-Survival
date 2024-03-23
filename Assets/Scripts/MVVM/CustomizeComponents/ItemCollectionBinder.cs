using UnityEngine.UIElements;

namespace Project.MVVM
{
    public abstract class ItemCollectionBinder<TItem, TElement> : IItemCollectionBinder where TElement : VisualElement
    {
        protected abstract void Bind(TItem item, TElement element);

        protected abstract void Unbind(TItem item, TElement element);

        public void Bind(object item, VisualElement element) => Bind((TItem)item, (TElement)element);

        public void Unbind(object item, VisualElement element) => Unbind((TItem)item,(TElement)element);
    }

    public interface IItemCollectionBinder
    {
        public void Bind(object item, VisualElement element);
        public void Unbind(object item, VisualElement element);
    }
}