using System;
using UnityEngine;

namespace Project.SpawnSystem
{
    [System.Serializable]
    public struct Rate : IComparable<Rate>
    {
        private const int ScalingFactor = 100;

        [UnityEngine.SerializeField, Range(0, 100)]private int value;

        public Rate(float value)
        {
            this.value = (int)(value * ScalingFactor);
        }
        public Rate(int value)
        {
            this.value = value;
        }

        public static implicit operator Rate(float value)
        {
            return new Rate(value);
        }

        public static implicit operator Rate(int value)
        {
            return new Rate(value);
        }

        public static implicit operator int(Rate percentage)=>percentage.value;

        public static implicit operator float(Rate percentage)
        {
            return percentage.value / (float)ScalingFactor;
        }

        public static Rate operator +(Rate p1, Rate p2)
        {
            return new Rate(p1.value + p2.value);
        }

        public static Rate operator +(Rate pValue, float fValue)
        {
            return new Rate(pValue.value + fValue * ScalingFactor);
        }
        public static Rate operator +(Rate pValue, int intValue)
        {
            return new Rate(pValue.value + intValue);
        }

        public static Rate operator -(Rate p1, Rate p2)
        {
            return new Rate(p1.value - p2.value);
        }

        public static Rate operator -(Rate pValue, float fValue)
        {
            return new Rate(pValue.value - fValue * ScalingFactor);
        }
        public static Rate operator -(Rate pValue, int intValue)
        {
            return new Rate(pValue.value - intValue);
        }

        public static float operator *(float fValue, Rate pValue)
        {
            return (float)pValue.value * fValue;
        }

        public readonly int CompareTo(Rate other)
        {
            return value.CompareTo(other.value);
        }

        // ... other operators as needed (-, *, /, etc.)

    }
}