namespace Project.AnimationEventSystem
{
    public interface IEventDataConverter
    {
        object Convert(AnimationEventData data);
    }
}