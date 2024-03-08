using CommunityToolkit.Mvvm.Messaging;
using MVVMToolkit;
using MVVMToolkit.Messaging;
using UnityEngine.UIElements;

namespace Project.MVVM.PlayerHUD
{
    public class PlayerHUDView : BaseView
    {
        protected override VisualElement Instantiate(){
            var root = base.Instantiate();
            return root;
        }
    }
}