using Project.CharacterBehaviour;
namespace Project.Enemy
{
    public interface IAIBehaviour
    {
        void OnTargetDetected(Core target);
        void Update();
        bool enabled { get; set; }
    }
}