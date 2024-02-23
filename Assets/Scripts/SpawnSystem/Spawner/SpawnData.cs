using System;
using UnityEngine;

namespace Project.SpawnSystem
{
    public enum SpawnType : byte{
        Regular, // spawn enemies surrounding the player
        Wave, // more complex spawn (based on shape, target, stop conditions)
        Boss, // Specific this to perform further events after killing the boss: win the game, kill all enemies, collect all exp
    }
    public enum SpawnPosition : byte{
        AroundPlayer, // typically, spawn out side of cam view randomly
        DistanceFromPlayer, // spawn at specific position based on given distance from the player
        GivenPosition, // spawn at specific position: useful for trigger events: open chest trap, unlock the door
    }

    public struct SpawnRate{
        public Rate Rate;
        public uint EntityID;

        public SpawnRate(Rate rate, uint entityID){
            this.Rate = rate;
            this.EntityID = entityID;
        }
    }

    [System.Serializable]
    public class SpawnData{
        [SerializeField] private SpawnType _spawnType;
        public SpawnType SpawnType => _spawnType;
        [SerializeField] private IndividualData[] _individualData;
        public IndividualData[] IndividualData => _individualData;
        [SerializeField] private WaveData _waveData; // available only when SpawnType is Wave
        public WaveData WaveData => _waveData; 
        [SerializeField] private UnityEngine.Events.UnityEvent _postEventBoss; // available only when SpawnType is Boss
        public UnityEngine.Events.UnityEvent PostEventBoss => _postEventBoss;
        [SerializeField] private SpawnContextData _contextData;
        public SpawnContextData ContextData => _contextData;
    }

    /// <summary>
    /// This class contains some configurations about wave
    /// </summary>
    [System.Serializable]
    public class WaveData{
        [SerializeField]private uint _entityID;
        public uint EntityID => _entityID;

        [SerializeField] private int _count;
        public int Count => _count;

        [SerializeField] private DropItem[] _dropData;
        public DropItem[] DropData => _dropData;
        // Form to align enemies
        // guide the enemies how to move
        // stop conditions: time, distance
        // enemy count
    }


    [System.Serializable]
    public class IndividualData{
        [SerializeField] private uint _entityID;
        public uint EntityID => _entityID;
        [SerializeField] private Rate _spawnRate;
        public Rate SpawnRate => _spawnRate;

        [SerializeField] private DropItem[] _dropItems;
        public DropItem[] DropItems => _dropItems;

        [SerializeField] private uint _expAmount;
        public uint ExpAmount => _expAmount;

        [SerializeField] private uint _goldAmount;
        public uint GoldAmount => _goldAmount;
    }
}