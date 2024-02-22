namespace Project.GameFlowSystem
{
    /// <summary>
    /// define a link from state to other state
    /// </summary>
    public interface IGameLink{
        bool ValidateTransition(out IGameState nextState);
        void Enable();
        void Disable();
    }

    public abstract class DefaultGameLink : IGameLink
    {
        private readonly IGameState _nextState;
        public DefaultGameLink(IGameState nextState){
            _nextState = nextState;
        }

        public virtual void Disable(){}
        public virtual void Enable(){}

        public bool ValidateTransition(out IGameState nextState){
            bool result = InnerValidation();
            nextState = result == true ? _nextState : null;
            return result;
        }

        protected abstract bool InnerValidation();

    }
}