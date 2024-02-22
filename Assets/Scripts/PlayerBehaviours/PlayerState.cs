using Project.BuffSystem;

namespace Project.PlayerBehaviour
{
    public class PlayerStats : IStatSetter, IStatGetter
    {
        public uint Health { get; set; }
        public uint Mana { get; set; }
        public Percentage MoveSpeed { get; set; }
        public uint Attack { get; set; }
        public uint Defense { get; set; }
        public Percentage CriticalRate { get; set; }

        public void SetStat<TValue>(StatType type, TValue value)
        {
            try
            {

                switch (type)
                {
                    case StatType.Health: Health = (uint)(object)value; break;
                    case StatType.Mana: Mana = (uint)(object)value; break;
                    case StatType.MoveSpeed: MoveSpeed = (float)(object)value; break;
                    case StatType.Attack: Attack = (uint)(object)value; break;
                    case StatType.Defense: Defense = (uint)(object)value; break;
                    case StatType.CriticalRate: CriticalRate = (float)(object)value; break;
                }
            }
            catch
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log($"can't set stat {type} with {value}");
#endif
            }
        }

        public bool TryGetStat<TValue>(StatType type, out TValue value)
        {
            try
            {
                switch (type)
                {
                    default:
                        {
#if UNITY_EDITOR
                            UnityEngine.Debug.Log($"can't find stat with type: {type}");
#endif
                            value = default;
                            return false;
                        }
                    case StatType.Health:
                        {
                            value = (TValue)(object)Health;
                            break;
                        }
                    case StatType.Mana:
                        {
                            value = (TValue)(object)Mana;
                            break;
                        }
                    case StatType.MoveSpeed:
                        {
                            value = (TValue)(object)MoveSpeed;
                            break;
                        }
                    case StatType.Attack:
                        {
                            value = (TValue)(object)Attack;
                            break;
                        }
                    case StatType.Defense:
                        {
                            value = (TValue)(object)Defense;
                            break;
                        }
                    case StatType.CriticalRate:
                        {
                            value = (TValue)(object)CriticalRate;
                            break;
                        }
                }
                return true;
            }
            catch
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log($"can't convert stat type {type} with value: {typeof(TValue)}");
#endif
                value = default;
                return false;
            }
        }
    }

    public class PlayerBuffs : IBuffPlus, IBuffMul
    {
        public int HealthPlus { get; set; }
        public Percentage HealthMul { get; set; }
        public int ManaPlus { get; set; }
        public Percentage ManaMul { get; set; }
        public Percentage MoveSpeedMul { get; set; }
        public int DamagePlus { get; set; }
        public Percentage DamageMul { get; set; }
        public int DefensePlus { get; set; }
        public Percentage DefenseMul { get; set; }
        public Percentage CriticalRateMul { get; set; }

        public bool TryAddPlus<TValue>(StatType statType, TValue value)
        {
            try
            {

                switch (statType)
                {
                    case StatType.Health: HealthPlus += (int)(object)value; break;
                    case StatType.Mana: ManaPlus += (int)(object)value; break;
                    case StatType.Attack: DamagePlus += (int)(object)value; break;
                    case StatType.Defense: DefensePlus += (int)(object)value; break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryGetPlus<TValue>(StatType statType, out TValue value)
        {
            try
            {

                switch (statType)
                {
                    case StatType.Health:
                        {
                            value = (TValue)(object)HealthPlus;
                            return true;
                        }
                    case StatType.Mana:
                        {
                            value = (TValue)(object)ManaPlus;
                            return true;
                        }
                    case StatType.Attack:
                        {
                            value = (TValue)(object)DamagePlus;
                            return true;
                        }
                    case StatType.Defense:
                        {
                            value = (TValue)(object)DefensePlus;
                            return true;
                        }
                    default:
                        {
                            value = default;
                            return false;
                        }
                };
            }
            catch
            {
                value = default;
                return false;
            }
        }

        public bool TryAddMul<TValue>(StatType statType, TValue value)
        {
            try
            {
                switch (statType)
                {
                    case StatType.MoveSpeed: MoveSpeedMul += (float)(object)value; break;
                    case StatType.CriticalRate: CriticalRateMul += (float)(object)value; break;
                    case StatType.Attack: DamageMul += (float)(object)value; break;
                    case StatType.Defense: DefenseMul += (float)(object)value; break;
                    case StatType.Mana: ManaMul += (float)(object)value; break;
                    case StatType.Health: HealthMul += (float)(object)value; break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryGetMul<TValue>(StatType statType, out TValue value)
        {
            try
            {
                // chang this to switch case
                switch (statType)
                {
                    case StatType.MoveSpeed:
                        {
                            value = (TValue)(object)MoveSpeedMul;
                            return true;
                        }
                    case StatType.CriticalRate:
                        {
                            value = (TValue)(object)CriticalRateMul;
                            return true;
                        }
                    case StatType.Attack:
                        {
                            value = (TValue)(object)DamageMul;
                            return true;
                        }
                    case StatType.Defense:
                        {
                            value = (TValue)(object)DefenseMul;
                            return true;
                        }
                    case StatType.Mana:
                        {
                            value = (TValue)(object)ManaMul;
                            return true;
                        }
                    case StatType.Health:
                        {
                            value = (TValue)(object)HealthMul;
                            return true;
                        }
                    default:
                        {
                            value = default;
                            return false;
                        }

                }
            }
            catch
            {
                value = default;
                return false;
            }
        }

        public bool TrySubPlus<TValue>(StatType statType, TValue value)
        {
            try
            {
                switch (statType)
                {
                    case StatType.Health: HealthPlus -= (int)(object)value; break;
                    case StatType.Mana: ManaPlus -= (int)(object)value; break;
                    case StatType.Attack: DamagePlus -= (int)(object)value; break;
                    case StatType.Defense: DefensePlus -= (int)(object)value; break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TrySubMul<TValue>(StatType statType, TValue value)
        {
            try
            {
                switch (statType)
                {
                    case StatType.MoveSpeed: MoveSpeedMul -= (float)(object)value; break;
                    case StatType.CriticalRate: CriticalRateMul -= (float)(object)value; break;
                    case StatType.Attack: DamageMul -= (float)(object)value; break;
                    case StatType.Defense: DefenseMul -= (float)(object)value; break;
                    case StatType.Mana: ManaMul -= (float)(object)value; break;
                    case StatType.Health: HealthMul -= (float)(object)value; break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}