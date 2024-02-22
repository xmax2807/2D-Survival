namespace Project.BuffSystem
{
    public enum StatType{
        Health,
        Mana,
        MoveSpeed,
        Attack,
        Defense,
        CriticalRate,
    }
    public struct Stat{
        public object value {get;}
        public StatType type;
        public Stat(int value, StatType type){
            this.value = value;
            this.type = type;
        }

        public Stat(float value, StatType type){
            this.value = value;
            this.type = type;
        }
    }
    public interface IBuffGetter{
        bool TryGetBuff<TValue>(BuffType type, out TValue value);
    }

    public interface IBuffSetter{
        void SetBuff<TValue>(BuffType type, TValue value);
    }

    
}