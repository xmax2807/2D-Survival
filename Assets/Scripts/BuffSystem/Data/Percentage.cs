using UnityEngine;

namespace Project.BuffSystem
{
    [System.Serializable]
    public struct Percentage
    {
        private const int ScalingFactor = 100;

        [UnityEngine.SerializeField,Range(0,100)]private int value;

        public Percentage(float value)
        {
            this.value = (int)(value * ScalingFactor);
        }
        public Percentage(int value)
        {
            this.value = value;
        }

        public static implicit operator Percentage(float value)
        {
            return new Percentage(value);
        }

        public static implicit operator Percentage(int value)
        {
            return new Percentage(value);
        }

        public static implicit operator float(Percentage percentage)
        {
            return percentage.value / (float)ScalingFactor;
        }

        public static Percentage operator +(Percentage p1, Percentage p2)
        {
            return new Percentage(p1.value + p2.value);
        }

        public static Percentage operator +(Percentage pValue, float fValue)
        {
            return new Percentage(pValue.value + fValue * ScalingFactor);
        }
        public static Percentage operator +(Percentage pValue, int intValue)
        {
            return new Percentage(pValue.value + intValue);
        }

        public static Percentage operator -(Percentage p1, Percentage p2)
        {
            return new Percentage(p1.value - p2.value);
        }

        public static Percentage operator -(Percentage pValue, float fValue)
        {
            return new Percentage(pValue.value - fValue * ScalingFactor);
        }
        public static Percentage operator -(Percentage pValue, int intValue)
        {
            return new Percentage(pValue.value - intValue);
        }

        public static float operator *(float fValue, Percentage pValue)
        {
            return (float)pValue.value * fValue;
        }

        // ... other operators as needed (-, *, /, etc.)

    }
}