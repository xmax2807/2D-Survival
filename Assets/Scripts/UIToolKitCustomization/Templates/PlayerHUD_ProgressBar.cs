using System;
using System.Collections.Generic;
using Project.UIToolKit.Asset;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    public class PlayerHUD_ProgressBar : VisualElement, INotifyValueChanged<int>
    {
        readonly ProgressBarAssetDefinition _config;
        VisualElement _head;
        VisualElement _middleContainer;
        VisualElement _tail;
        readonly int _baseMaxValue;
        private float currentPartitials = 0;

        int _maxValue = 1;
        public int maxValue {get => _maxValue; set => SetMaxValue(value);}

        private int _value;
        public int value { get => _value; set => SetValue(value); }

        public PlayerHUD_ProgressBar(ProgressBarAssetDefinition config){
            _config = config;
            _baseMaxValue = config.baseMaxValue;
            this.style.width = new StyleLength(StyleKeyword.Auto);
            this.pickingMode = PickingMode.Ignore;

            DefineDirection(this, config.direction);
            _head = new VisualElement();
            DefineStyle(_head);
            _head.style.backgroundImage = new StyleBackground(config.headBackground);
            
            _middleContainer = new VisualElement();
            DefineDirection(_middleContainer, config.direction);
            // var partitialMiddle = new VisualElement();
            // DefineStyle(partitialMiddle);
            // partitialMiddle.style.backgroundImage = new StyleBackground(config.middleBackground);
            // _middleContainer.Add(partitialMiddle);

            _tail = new VisualElement();
            DefineStyle(_tail);
            _tail.style.backgroundImage = new StyleBackground(config.tailBackground);
            // _tail.style.unitySliceScale = 2.5f;


            this.Add(_head);
            this.Add(_middleContainer);
            this.Add(_tail);

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanelEvent);
        }

        private void DefineDirection(VisualElement target,ProgressBarAssetDefinition.Direction direction){
            FlexDirection flexDirection = FlexDirection.Row;
            switch (direction){
                case ProgressBarAssetDefinition.Direction.LeftToRight:
                    flexDirection = FlexDirection.Row;
                    break;
                case ProgressBarAssetDefinition.Direction.RightToLeft:
                    flexDirection = FlexDirection.RowReverse;
                    break;
                case ProgressBarAssetDefinition.Direction.BottomToTop:
                    flexDirection = FlexDirection.Column;
                    break;
                case ProgressBarAssetDefinition.Direction.TopToBottom:
                    flexDirection = FlexDirection.ColumnReverse;
                    break;
            }

            target.style.flexDirection = flexDirection;
        }

        public void SetValueWithoutNotify(int newValue)
        {
            value = newValue;
        }


        private void SetValue(int newValue)
        {
            if(newValue != _value){
                _value = newValue;
                UpdateUI();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate">from 0 to 1</param>
        /// <param name="delta">from 0.0001 to 1</param>
        private void UpdateUI()
        {
            int maxCount = (int)(_maxValue / _baseMaxValue);
            maxCount = Mathf.Max(1, maxCount);
            int partitialCount = (int)(_value/ _baseMaxValue);

            UnityEngine.Sprite headBackgroundSprite = partitialCount == 0 ? _config.headBackground : _config.headFill;
            _head.style.backgroundImage = new StyleBackground(headBackgroundSprite);

            float i = 1;
            foreach(VisualElement e in _middleContainer.Children()){
                if(i < partitialCount){
                    e.style.backgroundImage = new StyleBackground(_config.middleFill);
                }else{
                    e.style.backgroundImage = new StyleBackground(_config.middleBackground);
                }
                ++i;
            }

            UnityEngine.Sprite tailBackgroundSprite = partitialCount == maxCount ? _config.tailFill : _config.tailBackground;
            _tail.style.backgroundImage = new StyleBackground(tailBackgroundSprite);
        }

        private void DefineStyle(VisualElement target){
            target.pickingMode = PickingMode.Ignore;
            target.style.flexShrink = 0;
            // target.style.width = new StyleLength(_config.size.x);
            // target.style.height = new StyleLength(_config.size.y);
            target.style.backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Center);
            target.style.backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Center);
            target.style.backgroundRepeat = new BackgroundRepeat(Repeat.NoRepeat, Repeat.NoRepeat);
            target.style.backgroundSize = new BackgroundSize(BackgroundSizeType.Contain);
        }

        private void SetMaxValue(int value)
        {
            if(value == 0 || value == _maxValue) return;
            _maxValue = value;
            int newMaxCount = _maxValue / this._baseMaxValue;
            newMaxCount = Mathf.Max(1, newMaxCount);
            AddOrRemoveMiddlePartitials(newMaxCount - 2); //-2 because head and tail
            UpdateUI();
        }

        private void AddOrRemoveMiddlePartitials(int count)
        {
            if(count < 0) count = 0;
            int childCount = _middleContainer.childCount;

            //TODO: pool this

            if(childCount > count){
                for(int i = childCount - 1; i >= count; --i){
                    _middleContainer.RemoveAt(i);
                }
            }
            else if(childCount < count){
                for(int i = childCount; i < count; ++i){
                    var ele = new VisualElement();
                    DefineStyle(ele);
                    ele.style.backgroundImage = new StyleBackground(_config.middleBackground);
                    _middleContainer.Add(ele);
                }
            }
        }

        private void OnAttachToPanelEvent(AttachToPanelEvent evt)
        {
            parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            OnGeometryChanged(null);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            FitToParent(_head, Vector2Int.one);
            _middleContainer.style.height = Mathf.Floor(this.resolvedStyle.height);
            foreach(VisualElement e in _middleContainer.Children()){
                e.style.width = Mathf.Floor(this.resolvedStyle.height);
            }
            FitToParent(_tail, Vector2Int.one);
        }

        void FitToParent(VisualElement target, Vector2Int aspectRatio)
		{
			if (target.parent == null) return;
			var parentW = target.parent.resolvedStyle.width;
			var parentH = target.parent.resolvedStyle.height;
			if (float.IsNaN( parentW ) || float.IsNaN( parentH )) return;

			// target.style.position = Position.Absolute;
			// target.style.left = 0;
			// target.style.top = 0;
			// target.style.right = StyleKeyword.Undefined;
			// target.style.bottom = StyleKeyword.Undefined;

			if (aspectRatio.x <= 0.0f || aspectRatio.y <= 0.0f)
			{
				target.style.width = parentW;
				target.style.height = parentH;
				return;
			}

			var ratio = Mathf.Min( parentW / aspectRatio.x, parentH / aspectRatio.y );
			var targetW = Mathf.Floor( aspectRatio.x * ratio );
			var targetH = Mathf.Floor( aspectRatio.y * ratio );
			target.style.width = targetW;
			target.style.height = targetH;
            

			// var marginX = parentW - targetW;
			// var marginY = parentH - targetH;
			// target.style.left = Mathf.Floor( marginX * balance.x / 100.0f );
			// target.style.top = Mathf.Floor( marginY * balance.y / 100.0f );
		}
    }
}