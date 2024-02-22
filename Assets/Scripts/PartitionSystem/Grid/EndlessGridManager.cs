using System;
using System.Collections;
using System.Collections.Generic;
using Project.Pooling;
using UnityEngine;

namespace Project.PartitionSystem
{
    public class EndlessGridManager
    {
        public readonly static Vector2Int StartPosition = Vector2Int.zero;
        public const int NEIGHBOR_DEPTH = 1;
        private ITrackedTarget m_trackedTarget;
        private readonly int chunkSize;
        private ChunkStorage m_chunkStorage;
        readonly Dictionary<Vector2Int, Cell> activeCells; // Vector2Int: cell position
        readonly Dictionary<int, Chunk> loadedChunks; //int: chunk id

        public Vector2Int CurrentChunkPosition{get; private set;}


        public EndlessGridManager(ChunkStorage chunkStorage, int chunkSize){
            m_chunkStorage = chunkStorage;
            this.chunkSize = chunkSize;
            loadedChunks = new Dictionary<int, Chunk>();
            activeCells = new Dictionary<Vector2Int, Cell>();
        }

        public void OnTrackedTargetChanged(ITrackedTarget target){
            if(m_trackedTarget == target || target == null){ 
                return;
            }
            m_trackedTarget = target;
            UpdateCurrentChunk();
        }

        private void UpdateCurrentChunk()
        {
            CurrentChunkPosition = GetCellPositionAtTarget(m_trackedTarget.position);
        }

        public IEnumerator Update(){
            if(m_trackedTarget == null){
                yield break;
            }

            Vector2Int cellPosition = GetCellPositionAtTarget(m_trackedTarget.position);
            List<Vector2Int> needLoadCells = QuickListPool<Vector2Int>.GetList();
            for(int i = -NEIGHBOR_DEPTH; i <= NEIGHBOR_DEPTH; ++i){
                for(int j = -NEIGHBOR_DEPTH; j <= NEIGHBOR_DEPTH; ++j){
                    needLoadCells.Add(new Vector2Int(cellPosition.x + i, cellPosition.y + j));
                }
            }

            // loop dictionary and find chunks that need to be unloaded
            List<Chunk> needUnloadChunks = QuickListPool<Chunk>.GetList();
            List<Vector2Int> needUnloadLoadCells = QuickListPool<Vector2Int>.GetList();
            foreach(var chunkPosition in activeCells.Keys){
                if(!needLoadCells.Contains(chunkPosition)){
                    needUnloadChunks.Add(loadedChunks[activeCells[chunkPosition].ChunkId]);
                    needUnloadLoadCells.Add(chunkPosition);
                }
            }

            // unload chunks and remove from activeCells
            for(int i = needUnloadChunks.Count - 1; i >= 0; --i){
                var chunk = loadedChunks[needUnloadChunks[i].ChunkId];
                chunk.ChunkObject.SetActive(false);
                m_chunkStorage.ReturnChunk(needUnloadChunks[i].ChunkId, loadedChunks[needUnloadChunks[i].ChunkId]);
                
                //TODO call event (chunk unloaded)
                activeCells.Remove(needUnloadLoadCells[i]);
            }

            yield return null;

            //load chunks and place at given positions
            for(int i = needLoadCells.Count -1; i >= 0; --i){
                var chunk = m_chunkStorage.GetChunkRandomly();
                chunk.ChunkObject.transform.position = new Vector3(needLoadCells[i].x * chunkSize, needLoadCells[i].y * chunkSize);
                chunk.ChunkObject.SetActive(true);

                //TODO: call event (chunk loaded)
                activeCells.Add(needLoadCells[i], new Cell(needLoadCells[i], chunk.ChunkId));

                yield return null;
            }

            QuickListPool<Vector2Int>.ReturnList(needLoadCells);
            QuickListPool<Vector2Int>.ReturnList(needUnloadLoadCells);
            QuickListPool<Chunk>.ReturnList(needUnloadChunks);
        }

        public Vector2Int GetCellPositionAtTarget(Vector2 targetPosition){
            float xDistance = targetPosition.x - StartPosition.x;
            float yDistance = targetPosition.y - StartPosition.y;


            int xPosition = (int)(xDistance / chunkSize);
            int yPosition = (int)(yDistance / chunkSize);

            return new Vector2Int(xPosition, yPosition);
        }
    }
}