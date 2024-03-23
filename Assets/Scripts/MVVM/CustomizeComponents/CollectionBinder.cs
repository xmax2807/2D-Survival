using System;
using UnityEngine.UIElements;
using System.Collections;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Specialized;
using MVVMToolkit.Binding;
namespace Project.MVVM
{
    public abstract class CollectionBinder<TElement> : ICollectionBinder where TElement : VisualElement
    {
        public Type Type => typeof(TElement);
        protected TElement CollectionElement { get; private set; }

        protected IList Data { get; private set; }
        protected INotifyCollectionChanged Notifier { get; private set; }
        protected DataTemplate DataTemplate { get; private set; }
        protected IRelayCommand Command { get; private set; }

        protected abstract void BindCollection();
        protected abstract void UnbindCollection();

        protected void CollectionBindItem(VisualElement element, int i)
        {
            var runtimeTemplate = (RuntimeTemplate)element;
            foreach (var (child, binder) in runtimeTemplate.bindings)
            {
                binder.Bind(Data[i], child);
            }
        }

        protected void CollectionUnbindItem(VisualElement element, int i)
        {
            var runtimeTemplate = (RuntimeTemplate)element;
            foreach (var (child, binder) in runtimeTemplate.bindings)
            {
                binder.Unbind(Data[i], child);
            }
        }


        public void Bind(VisualElement view, IList data, DataTemplate template, IRelayCommand command)
        {
            if (data is not INotifyCollectionChanged notifier)
            {
                throw new BindingException(
                    $"{nameof(data)} does not implement {nameof(INotifyCollectionChanged)}");
            }

            Data = data;
            Notifier = notifier;
            CollectionElement = (TElement)view;
            DataTemplate = template;
            Command = command;
            BindCollection();
        }

        public void Dispose()
        {
            UnbindCollection();
            Data = null;
            Notifier = null;
            CollectionElement = null;
            Command = null;
        }
    }

    public interface ICollectionBinder : IElementBinding
    {
        public Type Type { get; }
        public void Bind(VisualElement view, IList data, DataTemplate template, IRelayCommand command);
    }
}