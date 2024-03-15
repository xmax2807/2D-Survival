using MVVMToolkit;
using Project.UIToolKit;
using Project.UIToolKit.Asset;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.MVVM.PlayerHUD
{
    public class PlayerHUDView : BaseView
    {
        [SerializeField] ProgressBarAssetDefinition healthProgressbarConfig;
        [SerializeField] AvatarAssetDefinition avatarAssetDefinition;
        protected override VisualElement Instantiate(){
            var root = base.Instantiate();
            var mainHUD = root.Q<VisualElement>("mainHUD");
            var avatar = new PlayerHUD_Avatar(avatarAssetDefinition);
            var healthBar = new PlayerHUD_ProgressBar(healthProgressbarConfig)
            {
                viewDataKey = "${^value=Health}{^maxValue=MaxHealth}"
            };
            mainHUD.Add(avatar);
            avatar.AddBar(healthBar);
            return root;
        }
    }
}