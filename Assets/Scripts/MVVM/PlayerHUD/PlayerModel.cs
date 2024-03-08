using CommunityToolkit.Mvvm.ComponentModel;
using MVVMToolkit.Binding.Generics;
namespace Project.MVVM.PlayerHUD
{
    public partial class PlayerModel : ObservableObject
    {
        [ObservableProperty] private int health;
    }

    [UnityEngine.Scripting.Preserve]
    public class PlayerModelSolver : SingleSolver<PlayerModel>{}
}