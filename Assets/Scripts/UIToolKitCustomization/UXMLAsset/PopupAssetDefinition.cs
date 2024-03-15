using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UIToolKit.Asset
{
    [CreateAssetMenu(fileName = "PopupAsset", menuName = "UIToolKit/Assets/PopupAsset")]
    public class PopupAssetDefinition : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] Sprite background;

        public Sprite Icon => icon;
        public Sprite Background => background;
    }
}
