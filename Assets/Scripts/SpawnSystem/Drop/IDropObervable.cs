namespace Project.SpawnSystem
{
    public interface IDropObservable
    {
        void OnDrop(DropData data, int targetId);
    }
}