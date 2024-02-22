namespace Project.SpawnSystem
{
    public interface IDropObservable
    {
        void OnDrop(DropData data, int targetId);
    }

    public class DefaultDropObservable : IDropObservable
    {
        private readonly IDropHandler m_dropHandler;
        public DefaultDropObservable(IDropHandler dropHandler){
            m_dropHandler = dropHandler;
        }
        public void OnDrop(DropData data, int targetId)
        {
            m_dropHandler.DropExp(data.ExpAmount, targetId);
            m_dropHandler.DropItems(data.items, targetId);
        }
    }
}