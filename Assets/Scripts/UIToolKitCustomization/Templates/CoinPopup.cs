using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UIToolKit
{
    public class CoinPopup : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CoinPopup, UxmlTraits> { }
        
        private VisualElement _coinIcon;

        #if UNITY_EDITOR
        public CoinPopup(){
            var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/TemplateAssets/SmallPopup.uxml");
            var container = treeAsset.Instantiate();
            hierarchy.Add(container);

            _coinIcon = container.Q<VisualElement>("Icon");
        }
        #endif
    }
}