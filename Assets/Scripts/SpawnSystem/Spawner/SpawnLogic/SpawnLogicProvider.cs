using System;
using UnityEditor;
using UnityEngine;
namespace Project.SpawnSystem
{
    [CreateAssetMenu(fileName = "SpawnLogicProvider", menuName = "SpawnSystem/SpawnLogicProvider")]
    public class SpawnLogicProvider : ScriptableObject
    {
        public enum SpawnLogicType{
            RateCalculator,
            ShapeAlignment,
            SpawnPosition
        }

        private Camera currentCam => Camera.main;

        private Lazy<ISpawnLogic> _rarityCalculator;
        private Lazy<ISpawnLogic> _spawnPositionLogic;
        
        void OnEnable(){
            _rarityCalculator = new Lazy<ISpawnLogic>(() => new RarityCalculator());
            _spawnPositionLogic = new Lazy<ISpawnLogic>(() => new PositionSpawnLogic(currentCam));
        }

        public ISpawnLogic GetSpawnLogic(SpawnLogicType spawnLogicType){
            switch (spawnLogicType){
                case SpawnLogicType.RateCalculator: return _rarityCalculator.Value;
                case SpawnLogicType.ShapeAlignment: break;
                case SpawnLogicType.SpawnPosition: return _spawnPositionLogic.Value;
            }

            return null;
        }
    }
}