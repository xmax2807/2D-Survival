using System.Collections.Generic;

namespace Project.BuffSystem
{
    public interface IStatModifier
    {
        IBuffPlus IBuffPlus { get; } // get all buffs that use plus operation
        IBuffMul IBuffMul { get; } // get all buffs that use mul operation
    }
    public interface IStatGetter{
        bool TryGetStat<TValue>(StatType type, out TValue value);
    }
    public interface IStatSetter{
        void SetStat<TValue>(StatType type, TValue value);
    }

    public interface IBuffPlus // buffs class has buff plus properties
    {
        bool TryAddPlus<TValue>(StatType statType, TValue value);
        bool TrySubPlus<TValue>(StatType statType, TValue value);
        bool TryGetPlus<TValue>(StatType buffType, out TValue value);
    }
    public interface IBuffMul // buffs class has buff mul properties
    {
        bool TryAddMul<TValue>(StatType statType, TValue value);
        bool TrySubMul<TValue>(StatType statType, TValue value);
        bool TryGetMul<TValue>(StatType buffType, out TValue value);
    }
}