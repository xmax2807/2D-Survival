using System;
using System.Collections;
using System.Collections.Generic;
using Project.MVVM;
using Project.UIToolkitCustomization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UIToolkit
{
    public class GridView : VisualElement, ICustomListView
    {
        private const string k_itemSelectedVariantUssClassName = "grid-view__item--selected";
        private const string k_containerClassName = "grid-view__container";
        private const string k_scrollViewClassName = "grid-view__scroll-view";
        private const float ScrollThresholdSquared = 25;
        internal static readonly int s_DefaultItemHeight = 30;
        internal static CustomStyleProperty<int> s_ItemHeightProperty = new("--unity-item-height");
        private IList m_ItemsSource;
        public IList itemsSource
        {
            get => m_ItemsSource;
            set
            {
                m_ItemsSource = value;
                Rebuild();
            }
        }

        private Func<VisualElement> m_MakeItem;
        public Func<VisualElement> makeItem
        {
            get => m_MakeItem;
            set
            {
                if (m_MakeItem == value) return;
                m_MakeItem = value;
                Rebuild();
            }
        }

        private Action<VisualElement, int> m_BindItem;
        public Action<VisualElement, int> bindItem
        {
            get => m_BindItem;
            set
            {
                if (m_BindItem == value) return;

                m_BindItem = value;
                RefreshItems();
            }
        }

        private Action<VisualElement, int> m_UnbindItem;
        public Action<VisualElement, int> unbindItem
        {
            get => m_UnbindItem;
            set => m_UnbindItem = value;
        }

        public event Action<IEnumerable<int>> selectedIndicesChanged;
        public event Action<IEnumerable<object>> selectionChanged;
        public event Action<IEnumerable<object>> onItemsChosen;
        private readonly List<int> m_SelectedIndices = new();
        private readonly List<object> m_SelectedItems = new();
        private readonly VisualElement m_container;
        private readonly ScrollView m_scrollView;
        private KeyboardNavigationManipulator m_NavigationManipulator;
        private int selectedIndex;
        private readonly SelectionType m_selectionType;
        private float resolvedItemHeight => m_ItemHeight;
        private float m_LastHeight;
        private Vector3 m_TouchDownPosition;

        private int m_ItemHeight = s_DefaultItemHeight;
        private int m_ItemCountInRow;

        public GridView()
        {
            m_scrollView = new ScrollView();
            m_scrollView.AddToClassList(k_scrollViewClassName);

            m_container = new VisualElement
            {
                focusable = true
            };
            m_container.AddToClassList(k_containerClassName);

            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            m_container.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            m_container.RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);

            m_scrollView.contentContainer.Add(m_container);
            Add(m_scrollView);

            m_selectionType = SelectionType.Single;
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            m_ItemHeight = m_container.childCount > 0 ? (int)m_container[0].resolvedStyle.height : s_DefaultItemHeight;
            m_LastHeight = m_scrollView.layout.height;
            m_ItemCountInRow = (int)(m_scrollView.layout.width / m_ItemHeight);
            Debug.Log($"OnGeometryChanged: item height = {m_ItemHeight}, item count in row = {m_ItemCountInRow}");
        }

        public void RefreshItem(int index)
        {
            if (index < 0 || index >= m_container.childCount)
            {
                return;
            }
            var item = m_container[index];

            m_UnbindItem?.Invoke(item, index);
            m_BindItem?.Invoke(item, index);
        }

        public void RefreshItems()
        {
            // check if m_ItemsSource is valid
            if (m_ItemsSource == null)
            {
                for (int i = 0; i < m_container.childCount; ++i)
                {
                    m_UnbindItem?.Invoke(m_container[i], i);
                }
                m_container.Clear();
                return;
            }

            if (m_ItemsSource.Count > m_container.childCount)
            {
                // add more items
                for (int i = m_container.childCount; i < m_ItemsSource.Count; ++i)
                {
                    VisualElement item = m_MakeItem.Invoke();
                    m_container.Add(item);
                }
            }
            else if (m_ItemsSource.Count < m_container.childCount)
            {
                // remove items
                for (int i = m_container.childCount - 1; i >= m_ItemsSource.Count; --i)
                {
                    m_UnbindItem?.Invoke(m_container[i], i);
                    m_container.RemoveAt(i);
                }
            }

            for (int i = 0; i < m_container.childCount; ++i)
            {
                RefreshItem(i);
            }
        }

        public void Rebuild()
        {
            for (int i = 0; i < m_container.childCount; ++i)
            {
                m_UnbindItem?.Invoke(m_container[i], i);
            }
            m_container.Clear();
            RefreshItems();
        }

        /// <summary>
        /// Sets the currently selected item.
        /// </summary>
        /// <param name="index">The item index.</param>
        public void SetSelection(int index)
        {
            if (index < 0)
            {
                ClearSelection();
                return;
            }
            int[] indices = System.Buffers.ArrayPool<int>.Shared.Rent(1);
            indices[0] = index;
            SetSelection(indices);
            System.Buffers.ArrayPool<int>.Shared.Return(indices);
        }

        /// <summary>
        /// Sets a collection of selected items.
        /// </summary>
        /// <param name="indices">The collection of the indices of the items to be selected.</param>
        public void SetSelection(IEnumerable<int> indices)
        {
            SetSelectionInternal(indices, true);
        }

        /// <summary>
        /// Deselects any selected items.
        /// </summary>
        public void ClearSelection()
        {
            if (!HasValidDataAndBindings())
                return;

            ClearSelectionWithoutValidation();
            NotifyOfSelectionChange();
        }

        internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
        {
            if (!HasValidDataAndBindings() || indices == null)
                return;

            ClearSelectionWithoutValidation();
            foreach (var index in indices)
                AddToSelectionWithoutValidation(index);

            if (sendNotification)
                NotifyOfSelectionChange();

        }

        private bool HasValidDataAndBindings()
        {
            return itemsSource != null && makeItem != null && bindItem != null;
        }

        private void ClearSelectionWithoutValidation()
        {
            m_SelectedIndices.Clear();
            m_SelectedItems.Clear();
        }

        private void AddToSelectionWithoutValidation(int index)
        {
            if (m_SelectedIndices.Contains(index))
                return;

            var item = m_ItemsSource[index];

            m_SelectedIndices.Add(index);
            m_SelectedItems.Add(item);
        }

        private void NotifyOfSelectionChange()
        {
            if (!HasValidDataAndBindings())
                return;

            selectionChanged?.Invoke(m_SelectedItems);
            selectedIndicesChanged?.Invoke(m_SelectedIndices);
        }

        private void SelectItem(VisualElement element, bool selected)
        {
            if (selected)
            {
                element.AddToClassList(k_itemSelectedVariantUssClassName);
            }
            else
            {
                element.RemoveFromClassList(k_itemSelectedVariantUssClassName);
            }
        }

        protected override void ExecuteDefaultAction(EventBase evt)
        {
            base.ExecuteDefaultAction(evt);

            // We always need to know when pointer up event occurred to reset DragEventsProcessor flags.
            // Some controls may capture the mouse, but the ListView is a composite root (isCompositeRoot),
            // and will always receive ExecuteDefaultAction despite what the actual event target is.
            if (evt.eventTypeId == PointerUpEvent.TypeId())
            {
                //m_Dragger?.OnPointerUp();
            }
            // We need to store the focused item in order to be able to scroll out and back to it, without
            // seeing the focus affected. To do so, we store the path to the tree element that is focused,
            // and set it back in Setup().
            // else if (evt.eventTypeId == FocusEvent.TypeId())
            // {
            //     //m_LastFocusedElementTreeChildIndexes.Clear();

            //     if (m_scrollView.contentContainer.FindElementInTree(evt.target as VisualElement, m_LastFocusedElementTreeChildIndexes))
            //     {
            //         var recycledElement = m_ScrollView.contentContainer[m_LastFocusedElementTreeChildIndexes[0]];
            //         foreach (var recycledItem in m_Pool)
            //         {
            //             if (recycledItem.element == recycledElement)
            //             {
            //                 m_LastFocusedElementIndex = recycledItem.index;
            //                 break;
            //             }
            //         }

            //         m_LastFocusedElementTreeChildIndexes.RemoveAt(0);
            //     }
            //     else
            //     {
            //         m_LastFocusedElementIndex = -1;
            //     }
            // }
            // else if (evt.eventTypeId == NavigationSubmitEvent.TypeId())
            // {
            //     if (evt.target == this)
            //     {
            //         m_ScrollView.contentContainer.Focus();
            //     }
            // }
        }

        #region Events

        private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
        {
            if (e.customStyle.TryGetValue(s_ItemHeightProperty, out int height))
            {
                if (m_ItemHeight != height)
                {
                    m_ItemHeight = height;
                    RefreshItems();
                }
            }
        }
        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (evt.destinationPanel == null)
                return;

            m_NavigationManipulator = new KeyboardNavigationManipulator(Apply);
            m_container.AddManipulator(m_NavigationManipulator);
            m_container.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            m_container.RegisterCallback<PointerDownEvent>(OnPointerDown);
            m_container.RegisterCallback<PointerCancelEvent>(OnPointerCancel);
            m_container.RegisterCallback<PointerUpEvent>(OnPointerUp);

            m_container.Focus();
        }

        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            if (evt.originPanel == null)
                return;

            m_container.RemoveManipulator(m_NavigationManipulator);
            m_container.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            m_container.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            m_container.UnregisterCallback<PointerCancelEvent>(OnPointerCancel);
            m_container.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            ProcessPointerUp(evt);
        }

        private void OnPointerCancel(PointerCancelEvent evt)
        {
            if (!HasValidDataAndBindings())
                return;

            if (!evt.isPrimary)
                return;

            ClearSelection();
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            ProcessPointerDown(evt);
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            // Support cases where PointerMove corresponds to a MouseDown or MouseUp event with multiple buttons.
            if (evt.button == (int)MouseButton.LeftMouse)
            {
                if ((evt.pressedButtons & (1 << (int)MouseButton.LeftMouse)) == 0)
                {
                    ProcessPointerUp(evt);
                }
                else
                {
                    ProcessPointerDown(evt);
                }
            }
        }

        private void Apply(KeyboardNavigationOperation operation, EventBase sourceEvent)
        {
            var shiftKey = (sourceEvent as KeyDownEvent)?.shiftKey ?? false;
            if (Apply(operation, shiftKey))
            {
                sourceEvent.StopPropagation();
                sourceEvent.PreventDefault();
            }
        }

        void HandleSelectionAndScroll(Vector2Int direction, bool shiftKey = false)
        {
            int index = FindIndexBasedOnPosition(direction, this.selectedIndex);
            if (index < 0 || index >= m_ItemsSource.Count)
                return;

            if (m_selectionType == SelectionType.Multiple && shiftKey && m_SelectedIndices.Count != 0)
            {
                // DoRangeSelection(index);
                //Do nothing for now
            }
            else
            {
                SelectItem(m_container[selectedIndex], false);
                selectedIndex = index;
                SelectItem(m_container[selectedIndex], true);
                SetSelection(selectedIndex);
            }

            ScrollToItem(index);
        }

        private bool Apply(KeyboardNavigationOperation operation, bool shiftKey)
        {
            if (!HasValidDataAndBindings())
            {
                return false;
            }

            switch (operation)
            {
                case KeyboardNavigationOperation.Submit:
                    onItemsChosen?.Invoke(m_SelectedItems);
                    ScrollToItem(selectedIndex);
                    return true;
                case KeyboardNavigationOperation.Previous: // move up
                    HandleSelectionAndScroll(Vector2Int.up, shiftKey);
                    break;
                case KeyboardNavigationOperation.Next: // move down
                    HandleSelectionAndScroll(Vector2Int.down, shiftKey);
                    break;
                case KeyboardNavigationOperation.MoveRight:
                    HandleSelectionAndScroll(Vector2Int.right, shiftKey);
                    break;
                case KeyboardNavigationOperation.MoveLeft:
                    HandleSelectionAndScroll(Vector2Int.left, shiftKey);
                    break;
            }

            return false;
        }

        private void ProcessPointerUp(IPointerEvent evt)
        {
            if (!HasValidDataAndBindings())
                return;

            if (!evt.isPrimary)
                return;

            if (evt.button != (int)MouseButton.LeftMouse)
                return;

            if (evt.pointerType != UnityEngine.UIElements.PointerType.mouse)
            {
                var delta = evt.position - m_TouchDownPosition;
                if (delta.sqrMagnitude <= ScrollThresholdSquared)
                {
                    DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
                }
            }
            else // multiple
            {
                // var clickedIndex = (int)(evt.localPosition.y / itemHeight);
                // if (m_selectionType == SelectionType.Multiple
                //     && !evt.shiftKey
                //     && !evt.actionKey
                //     && m_SelectedIndices.Count > 1
                //     && m_SelectedIndices.Contains(clickedIndex))
                // {
                //     SetSelection(clickedIndex);
                // }
            }
        }

        private void ProcessPointerDown(IPointerEvent evt)
        {
            if (!HasValidDataAndBindings())
                return;

            if (!evt.isPrimary)
                return;

            if (evt.button != (int)MouseButton.LeftMouse)
                return;

            if (evt.pointerType != UnityEngine.UIElements.PointerType.mouse)
            {
                m_TouchDownPosition = evt.position;
                return;
            }

            DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
        }
        #endregion

        #region Selection Handling
        private void ScrollToItem(int index)
        {
            // if (!HasValidDataAndBindings())
            //     throw new InvalidOperationException("Can't scroll without valid source, bind method, or factory method.");

            // if (m_VisibleItemCount == 0 || index < -1)
            //     return;

            // var pixelAlignedItemHeight = resolvedItemHeight;
            // if (index == -1)
            // {
            //     // Scroll to last item
            //     int actualCount = (int)(m_LastHeight / pixelAlignedItemHeight);
            //     if (itemsSource.Count < actualCount)
            //         m_scrollView.scrollOffset = new Vector2(0, 0);
            //     else
            //         m_scrollView.scrollOffset = new Vector2(0, (itemsSource.Count + 1) * pixelAlignedItemHeight);
            // }
            // else if (m_FirstVisibleIndex >= index)
            // {
            //     m_scrollView.scrollOffset = Vector2.up * (pixelAlignedItemHeight * index);
            // }
            // else // index > first
            // {
            //     var actualCount = (int)(m_LastHeight / pixelAlignedItemHeight);
            //     if (index < m_FirstVisibleIndex + actualCount)
            //         return;

            //     var d = index - actualCount + 1;    // +1 ensures targeted element is fully visible
            //     var visibleOffset = pixelAlignedItemHeight - (m_LastHeight - actualCount * pixelAlignedItemHeight);
            //     var yScrollOffset = pixelAlignedItemHeight * d + visibleOffset;

            //     m_scrollView.scrollOffset =  new Vector2(m_scrollView.scrollOffset.x, yScrollOffset);
            // }
        }

        private int FindIndexBasedOnPosition(Vector2Int direction, int currentIndex)
        {

            // need to know how many items in one row
            int result = 0;
            if (direction.y == 0) // move horizontally
            {
                result = currentIndex + direction.x;
            }
            else if (direction.x == 0) // move vertically
            {
                result = currentIndex - direction.y * this.m_ItemCountInRow;
            }

            return result;
        }

        private void DoSelect(Vector2 localPosition, int clickCount, bool actionKey, bool shiftKey)
        {
            var clickedIndex = (int)(localPosition.y / resolvedItemHeight);
            if (clickedIndex > m_ItemsSource.Count - 1)
                return;

            switch (clickCount)
            {
                case 1:
                    if (m_selectionType == SelectionType.None)
                        return;


                    if (m_selectionType == SelectionType.Single) // single
                    {
                        SetSelection(clickedIndex);
                    }
                    break;
                case 2:
                    if (onItemsChosen != null)
                    {
                        SetSelection(clickedIndex);

                        onItemsChosen.Invoke(m_SelectedItems);
                    }
                    break;
            }
        }
        #endregion
    }
}