using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
namespace Project.PartitionSystem
{
    public class TileMapStaticBaker : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] Sprite occupiedSprite;
        readonly List<Vector3Int> occupiedCells = new List<Vector3Int>();
        Tilemap _tileMap;
        public Tilemap TileMap => _tileMap;
        public void Bake(){
            if(_tileMap == null){
                _tileMap = GetComponent<Tilemap>();
            }
            
            foreach(Vector3Int pos in occupiedCells){
                _tileMap.SetTile(pos, null);
            }
            occupiedCells.Clear();

            Tile template = new Tile(){
                sprite = occupiedSprite
            };
            
            foreach(Transform childTrans in this.transform){
                var child = childTrans.GetComponent<Collider2D>();
                var bounds = child.bounds;

                Vector2 min = new Vector2(bounds.min.x, bounds.min.y);
                Vector2 max = new Vector2(bounds.max.x, bounds.max.y);
                
                Vector3Int minCell = _tileMap.WorldToCell(min);
                Vector3Int maxCell = _tileMap.WorldToCell(max);

                for(int x = minCell.x; x <= maxCell.x; x++){
                    for(int y = minCell.y; y <= maxCell.y; y++){
                        Vector3Int pos = new Vector3Int(x, y, 0);
                        occupiedCells.Add(pos);
                        _tileMap.SetTile(pos, template);
                    }
                }
            }
        }
        #endif
    }
}