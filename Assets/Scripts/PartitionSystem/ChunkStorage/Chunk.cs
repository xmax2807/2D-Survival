using UnityEngine;

namespace Project.PartitionSystem
{
    [System.Serializable]
    public class SerializedChunk
    {
        [SerializeField] GameObject m_prefab;
        public GameObject Prefab => m_prefab;

        [SerializeField] int chunk_id;
        public int ChunkId => chunk_id;
    }

    public class Chunk{
        public const int EMPTY_ID = -1;
        public readonly GameObject ChunkObject;
        public readonly int ChunkId;

        public Chunk(int id, GameObject chunkObject){
            if(id == EMPTY_ID) throw new System.ArgumentException("Chunk id cannot be empty");
            this.ChunkId = id;
            this.ChunkObject = chunkObject;
        }
    }
}