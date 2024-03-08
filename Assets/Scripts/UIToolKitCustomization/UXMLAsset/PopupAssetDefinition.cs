using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UIToolKit.Asset
{
    [CreateAssetMenu(fileName = "PopupAsset", menuName = "UIToolKit/Assets/PopupAsset")]
    public class PopupAssetDefinition : ScriptableObject
    {
        [SerializeField] Texture2D icon;
        [SerializeField] Texture2D background;

        public Texture2D Icon => icon;
        public Texture2D Background => background;
    }
}
