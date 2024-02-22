namespace Project.SpawnSystem
{
    public interface ISpawnSchedulerCommand{
        System.Collections.IEnumerator Execute(ISpawnSchedulerController scheduler);
    }
    public interface ICommandSelector{
        ISpawnSchedulerCommand Next();
        void SetNextCommand(ISpawnSchedulerCommand command);
    }
}