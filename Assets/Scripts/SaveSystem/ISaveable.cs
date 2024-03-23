namespace Project.SaveSystem{

    [MessagePack.Union(0, typeof(PlayerBehaviour.PlayerData))]
    [MessagePack.Union(1, typeof(TestSaveData))]
    [MessagePack.Union(2, typeof(PlayerBehaviour.PlayerInventoryData))]
    public interface ISaveable{
        Project.Utils.SerializableGuid Id { get; }
    }

    public interface ISaveBind {
        void Bind(ISaveable saveable);
    }
    
    public interface ISaveBind<TSaveable> : ISaveBind where TSaveable : ISaveable{
        void Bind(TSaveable saveable);
    }
}