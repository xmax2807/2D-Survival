namespace Project.SaveSystem{
    public interface ISaveable{
        Project.Utils.SerializableGuid Id { get; }
    }
    
    public interface ISaveBind<TSaveable> where TSaveable : ISaveable{
        Project.Utils.SerializableGuid Id { get; set; }
        void Bind(TSaveable saveable);
    }
}