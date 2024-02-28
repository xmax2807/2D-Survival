namespace Project.SaveSystem
{
    public interface IBindRegister
    {
        void OnBindRegistryCreated(IBindRegistry registry);
        void OnBindRegistryDestroyed(IBindRegistry registry);
    }
}