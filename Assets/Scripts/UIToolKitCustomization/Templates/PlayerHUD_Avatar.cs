using System;
using System.Linq;
using Project.UIToolKit.Asset;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    public class PlayerHUD_Avatar : VisualElement
    {
        readonly VisualElement _barContainer;
        readonly VisualElement _avatarContainer;
        public const int MAX_BAR_COUNT = 3;

        public PlayerHUD_Avatar(AvatarAssetDefinition config){
            this.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            this.pickingMode = PickingMode.Ignore;

            _avatarContainer = new VisualElement();
            _avatarContainer.pickingMode = PickingMode.Ignore;
            _avatarContainer.style.height = new StyleLength(StyleKeyword.Auto);
            _avatarContainer.style.flexGrow = 0; 
            _avatarContainer.style.backgroundImage = new StyleBackground(config.border);
            
            _barContainer = new VisualElement();
            _barContainer.pickingMode = PickingMode.Ignore;
            _barContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
            _barContainer.style.flexGrow = 1;

            this.Add(_avatarContainer);
            this.Add(_barContainer);

            RegisterCallback<AttachToPanelEvent>( OnAttachToPanelEvent );
            RegisterCallback<DetachFromPanelEvent>( OnDetachFromPanelEvent );
        }

        public void AddBar(VisualElement bar){
            if(_barContainer.Children().Count() >= MAX_BAR_COUNT){
                throw new InvalidOperationException($"Can't add more than {MAX_BAR_COUNT} bars");
            }
            bar.style.flexGrow = 0;
            bar.style.height = this.resolvedStyle.height / MAX_BAR_COUNT;
            _barContainer.Add(bar);
        }

        private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
        {
            parent?.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnAttachToPanelEvent(AttachToPanelEvent evt)
        {
            parent?.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            OnGeometryChanged(null);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            this.StretchToParentSize();
            FitToParent(this._avatarContainer, Vector2Int.one, new Vector2Int(0,0));
            foreach(VisualElement e in _barContainer.Children()){
                e.style.height = this.resolvedStyle.height / MAX_BAR_COUNT;
            }
        }

        void FitToParent(VisualElement target, Vector2Int aspectRatio, Vector2Int balance)
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

			var marginX = parentW - targetW;
			var marginY = parentH - targetH;
			// target.style.left = Mathf.Floor( marginX * balance.x / 100.0f );
			// target.style.top = Mathf.Floor( marginY * balance.y / 100.0f );
		}
    }
}