using System;
using Project.UIToolkitCustomization;
using UnityEngine.UIElements;

namespace Project.MVVM.Inventory{
    public class SlotElement : VisualElement{
        public new class UxmlFactory : UxmlFactory<SlotElement, UxmlTraits>{}
        public new class UxmlTraits : VisualElement.UxmlTraits{}

        #region class names
        public const string slot_class_name = "slot";
        public const string slot_frame_class_name = "slot__frame";
        public const string slot_icon_class_name = "slot__icon";
        public const string slot_stack_class_name = "slot__stack";
        #endregion

        readonly Image m_icon;
        readonly Label m_stack;

        public SlotElement(){
            AddToClassList(slot_class_name);
            m_stack = this.CreateChild(slot_frame_class_name).CreateChild<Label>(slot_stack_class_name);
            m_icon = this.CreateChild<Image>(slot_icon_class_name);
            focusable = true;
            RegisterEvents();
        }

        public void SetIcon(UnityEngine.Sprite sprite) => m_icon.sprite = sprite;
        public void SetStack(int stack) {
            if(stack > 0){
                m_stack.text = stack.ToString();
                m_stack.visible = true;
            }
            else{
                m_stack.visible = false;
            }
        }


        #region Events
        private void RegisterEvents()
        {
            //RegisterCallback<PointerDownEvent>(OnPointerDown);
            RegisterCallback<FocusInEvent>(OnFocusIn);
            RegisterCallback<FocusOutEvent>(OnFocusOut);
        }

        private void OnFocusOut(FocusOutEvent evt)
        {
            if(evt.target == this){
                UnityEngine.Debug.Log("OnFocusOut");
                evt.StopPropagation();
            }
        }

        private void OnFocusIn(FocusInEvent evt)
        {
            if(evt.target == this){
                UnityEngine.Debug.Log("OnFocusIn");
                evt.StopPropagation();
            }
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            UnityEngine.Debug.Log("OnPointerDown");
            evt.StopPropagation();
        }
        #endregion
    }
}