namespace Project.BuffSystem{
    public interface IEffectCondition{
        bool IsMet<TEventData>(ITarget target, TEventData eventData);
    }
    public abstract class GenericEffectCondition<TEventData> : IEffectCondition{
        public bool IsMet<TData>(ITarget target, TData eventData){
            if(eventData is TEventData realData){
                return InnerCheck(realData);
            }
            return false;
        }

        protected abstract bool InnerCheck(TEventData eventData);
    }
    public interface ICondition{
        bool IsMet(ITarget target);
        void OnAddModifierTo(ITarget target);
    }
}