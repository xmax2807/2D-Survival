using Project.SaveSystem;
using Project.Utils;

namespace Project.PlayerBehaviour
{
    public class PlayerDataBinding : ISaveBind<PlayerData>
    {
        public PlayerData PlayerData{get;private set;}

        public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();

        public void Bind(PlayerData saveable)
        {
            if(saveable == null) return;
            PlayerData = saveable;
            UnityEngine.Debug.Log($"Binding {Id} to {PlayerData}");
        }

        public void Bind(ISaveable saveable) => Bind(saveable as PlayerData);
    }
}