using Project.SaveSystem;

namespace Project.PlayerBehaviour
{
    public class PlayerInventoryDataBinding : ISaveBind<PlayerInventoryData>
    {
        public PlayerInventoryData PlayerInventoryData { get; private set; }
        public void Bind(PlayerInventoryData saveable)
        {
            if(saveable == null) return;
            PlayerInventoryData = saveable;

            UnityEngine.Debug.Log($"Binded to {PlayerInventoryData}");
        }

        public void Bind(ISaveable saveable) => Bind(saveable as PlayerInventoryData);
    }
}