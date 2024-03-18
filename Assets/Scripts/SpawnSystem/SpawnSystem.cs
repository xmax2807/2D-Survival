using UnityEngine;

namespace Project.SpawnSystem
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] DefaultSpawnSchedulerManager defaultSpawnSchedulerManager;

        async void Start(){
            var listSchedulers = await defaultSpawnSchedulerManager.GetAllSpawnSchedulerAsync();
            for(int i = 0; i < listSchedulers.Length; ++i){
                AddSpawnScheduler(listSchedulers[i]);
            }
        }

        public void AddSpawnScheduler(ISpawnScheduler scheduler){
            // if(scheduler == null || spawnSchedulers.Contains(scheduler)){
            //     return;
            // }
            // spawnSchedulers.Add(scheduler);

            StartCoroutine(scheduler.Schedule());
        }
    }
}