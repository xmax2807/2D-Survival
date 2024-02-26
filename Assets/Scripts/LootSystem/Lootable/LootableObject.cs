using UnityEngine;
namespace Project.LootSystem
{
    public interface ILootableObject
    {
        int Id { get; }
        void OnPickerLoot(GameObject picker);
    }
    public interface IAutoLootableObject : ILootableObject{
        event System.Action<IAutoLootableObject, GameObject> OnPickerApproachedEvent;
    }
}