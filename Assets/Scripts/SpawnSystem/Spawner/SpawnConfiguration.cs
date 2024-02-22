using UnityEngine;
namespace Project.SpawnSystem
{
    /// <summary>
    /// A config of individual spawn, spawn manager will hold list of this config with its scehduler
    /// There are many kinds of spawn: Time-based, Event-based along with spawn type: regular, wave, boss
    /// Typically, regular spawn will rely on time-based spawn, wave spawn will rely on event-based or time-based spawn
    /// To separate different kinds of spawn, need ISpawnScheduler encapsulates the spawn data and logic then add to manager
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnConfiguration", menuName = "SpawnSystem/SpawnConfiguration", order = 0)]
    public class SpawnConfiguration : ScriptableObject
    {
        [SerializeField] private SpawnScheduleData spawnSchedulerData;
        public SpawnScheduleData SpawnSchedulerData => spawnSchedulerData;
        [SerializeField] private SpawnData spawnData;
        public SpawnData SpawnData => spawnData;

        public SpawnType SpawnType => spawnData.SpawnType;
    }
}