namespace Project.BuffSystem{
    public enum BuffType : byte
    {
        HealthPlus,
        HealthMul,
        ManaPlus,
        ManaMul,
        MoveSpeedPlus,
        MoveSpeedMul,
        DamagePlus,
        DamageMul,
        DefensePlus,
        DefenseMul,
        CriticalRatePlus,
    }
    [System.Serializable]
    public struct IntBuffData{
        public BuffType buffType;
        public uint value;
    }

    [System.Serializable]
    public struct FloatBuffData{
        public BuffType buffType;
        public float value;
    }

    public class BuffData : IBuffSetter, IBuffGetter
    {
        public void SetBuff<TValue>(BuffType type, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetBuff<TValue>(BuffType type, out TValue value)
        {
            throw new System.NotImplementedException();
        }
    }
}