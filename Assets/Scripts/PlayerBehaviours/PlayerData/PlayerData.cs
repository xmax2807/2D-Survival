using MessagePack;
using Project.SaveSystem;
using Project.Utils;

namespace Project.PlayerBehaviour
{
    [System.Serializable, MessagePackObject(true)]
    public class PlayerData : ISaveable
    {
        public uint Health { get; set; }
        public float MoveSpeed { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float CriticalRate { get; set; }

        private SerializableGuid _id;
        public SerializableGuid Id => _id;

        public static PlayerData Randomize(){
            return new()
            {
                Health = (uint)UnityEngine.Random.Range(0, 100),
                MoveSpeed = UnityEngine.Random.Range(0f, 1f),
                Attack = UnityEngine.Random.Range(0f, 100f),
                Defense = UnityEngine.Random.Range(0f, 100f),
                CriticalRate = UnityEngine.Random.Range(0f, 100f),
            };
        }

        public PlayerData() {
            _id = SerializableGuid.NewGuid();
        }

        public PlayerData(PlayerData clone) : this(){
            Health = clone.Health;
            MoveSpeed = clone.MoveSpeed;
            Attack = clone.Attack;
            Defense = clone.Defense;
            CriticalRate = clone.CriticalRate;
        }

        public PlayerData(uint health, float moveSpeed, float attack, float defense, float criticalRate) : this(){
            Health = health;
            MoveSpeed = moveSpeed;
            Attack = attack;
            Defense = defense;
            CriticalRate = criticalRate;
        }

        public ISaveable CreateNew()
        {
            return new PlayerData();
        }

        public override string ToString()
        {
            return $"Health: {Health}, MoveSpeed: {MoveSpeed}, Attack: {Attack}, Defense: {Defense}, CriticalRate: {CriticalRate}";
        }
    }
}