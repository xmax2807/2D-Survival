using Project.Enemy;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class EnemyDropObservable : MonoBehaviour, IDropObservable
    {
        [SerializeField] private ScriptableDropManager dropManager;

        public void OnDrop(DropData data, int targetId)
        {
            dropManager.DropExp(data.ExpAmount, targetId);
            dropManager.DropItems(data.items, targetId);
        }
    }
}