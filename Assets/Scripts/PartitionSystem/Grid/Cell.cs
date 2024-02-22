using System.Collections.Generic;
using Project.Pooling;
using UnityEngine;
namespace Project.PartitionSystem
{
    public struct Cell
    {
        public Vector2Int Position;
        public int ChunkId;
        public Cell(Vector2Int position, int prefabId){
            Position = position;
            ChunkId = prefabId;
        }
        public static Cell EmptyCell => new() { Position = Vector2Int.zero, ChunkId = Chunk.EMPTY_ID };
        public bool IsEmpty(){
            return ChunkId == Chunk.EMPTY_ID;
        }
    }
}