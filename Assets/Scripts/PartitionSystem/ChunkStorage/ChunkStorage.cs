using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Project.Pooling;
namespace Project.PartitionSystem
{
    [CreateAssetMenu(fileName = "ChunkStorage", menuName = "PartitionSystem/ChunkStorage", order = 1)]
    public class ChunkStorage : ScriptableObject
    {
        [SerializeField] SerializedChunk[] chunks;
        Dictionary<int, CustomCreationPool<Chunk>> chunksMap;

        void OnDisable(){
            chunksMap = null;
        }

        public IEnumerator Initialize(){
            chunksMap = new Dictionary<int, CustomCreationPool<Chunk>>();

            for(int i = 0; i < chunks.Length; ++i){
                if(chunks[i] == null || chunksMap.ContainsKey(chunks[i].ChunkId)){
                    #if UNITY_EDITOR
                    Debug.LogError("Error in chunk storage. Chunk with id " + chunks[i].ChunkId + " is null or already in the list");
                    #endif
                    continue;
                }

                var pool = new CustomCreationPool<Chunk>(() =>
                {
                    if(chunks[i].Prefab == null){
                        return null;
                    }
                    var obj = Object.Instantiate(chunks[i].Prefab);
                    obj.SetActive(false);
                    return new Chunk(chunks[i].ChunkId, obj);
                });

                var chunk = pool.Get();
                pool.Return(chunk);

                chunksMap.Add(chunks[i].ChunkId, pool);

                yield return null;
            }
        }

        public Chunk GetChunkById(int id){
            if(chunksMap.ContainsKey(id) == false){
                return null;
            }
            return chunksMap[id].Get();
        }

        public Chunk GetChunkRandomly(){
            return chunksMap[Random.Range(0, chunksMap.Count)].Get();
        }

        public void ReturnChunk(int id, Chunk chunk){
            if(chunksMap.ContainsKey(id) == false){
                return;
            }
            chunksMap[id].Return(chunk);
        }
    }
}