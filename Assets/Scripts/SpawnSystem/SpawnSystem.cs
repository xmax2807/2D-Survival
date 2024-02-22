using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.SpawnSystem
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] private GameObject test;
        [SerializeField] int spawnEachCount = 100;
        [SerializeField] float randomRange = 0.5f;
        [SerializeField] DefaultSpawnSchedulerManager defaultSpawnSchedulerManager;

        List<ISpawnScheduler> spawnSchedulers = new();

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
        public int spawned = 0;

        IEnumerator SpawnAsync(int count){
            // measure time execution
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for(int i = 0; i < count; ++i){
                Vector3 randomThreshold = new Vector3(Random.Range(-1f, 1f) * randomRange, Random.Range(-1f, 1f) * randomRange);
                Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var gameObj = Instantiate(test, new Vector3(screenToWorldPoint.x + randomThreshold.x, screenToWorldPoint.y + randomThreshold.y), Quaternion.identity);
                gameObj.transform.parent = transform;

                if(sw.ElapsedMilliseconds > 100){
                    sw.Stop();
                    yield return null;
                    sw.Start();
                }
                
            }

            spawned += count;
        }
    }
}