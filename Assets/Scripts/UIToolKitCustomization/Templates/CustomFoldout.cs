using UnityEngine.UIElements;

namespace Project.UIToolkitCustomization{
    public class CustomFoldout : BindableElement, INotifyValueChanged<bool>{
        public override VisualElement contentContainer => m_Container;

        [UnityEngine.SerializeField] private bool m_Value;

        private VisualElement m_ToggleContainer;
        private Toggle m_Toggle;
        VisualElement m_Container;
        public string text {
            get => m_Toggle.text;
            set => m_Toggle.text = value;
        }
        public VisualElement additionalElement{
            get => m_ToggleContainer[1];
            set => m_ToggleContainer.Insert(1, value);
        }

        public bool value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value == value)
                    return;

                using (ChangeEvent<bool> evt = ChangeEvent<bool>.GetPooled(m_Value, value))
                {
                    evt.target = this;
                    SetValueWithoutNotify(value);
                    SendEvent(evt);
                }
            }
        }

        public void SetValueWithoutNotify(bool newValue)
        {
            m_Value = newValue;
            m_Toggle.value = m_Value;
            contentContainer.style.display = newValue ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public CustomFoldout(){
            m_Value = true;

            AddToClassList(Foldout.ussClassName);
            m_ToggleContainer = new VisualElement(){
                name = Foldout.toggleUssClassName,
                style = {flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row)}
            };
            m_Toggle = new Toggle
            {
                value = true
            };
            m_Toggle.RegisterValueChangedCallback((evt) =>
            {
                value = m_Toggle.value;
                evt.StopPropagation();
            });
            m_Toggle.AddToClassList(Foldout.toggleUssClassName);

            m_ToggleContainer.Add(m_Toggle);
            m_ToggleContainer.Add(new VisualElement());
            
            hierarchy.Add(m_ToggleContainer);

            m_Container = new VisualElement()
            {
                name = "unity-content",
            };
            m_Container.AddToClassList(Foldout.contentUssClassName);
            hierarchy.Add(m_Container);

        }
        
    }
}