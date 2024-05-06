namespace Project.GameEventSystem.EventCommand
{
    public interface IEventCommand
    {
        System.Collections.IEnumerator Execute(params int[] args);
    }
}