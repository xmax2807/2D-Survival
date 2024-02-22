namespace Project.GameParams
{
    public interface IParamCenter{
        void AddParamAPI<TInterface, TInstance>(TInstance instance) where TInterface : class where TInstance : TInterface;
    }
    public interface IParamProvider
    {
        TInterface GetParamAPI<TInterface>();
    }

    public interface IParamSubscription{
        void OnAddedToCenter(IParamCenter provider);
    }
}