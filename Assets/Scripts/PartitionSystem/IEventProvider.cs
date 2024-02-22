namespace Project.PartitionSystem
{
    public interface IEventProvider
    {
        event System.Action<ITrackedTarget> OnTrackedTargetChanged;
    }
}