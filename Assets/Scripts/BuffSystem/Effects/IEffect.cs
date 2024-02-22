namespace Project.BuffSystem
{
    public interface IEffect
    {
        void Apply(ITarget target);
        void Remove(ITarget target);
    }
}