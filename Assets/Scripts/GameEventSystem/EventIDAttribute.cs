using System;
using UnityEngine;

namespace Project.GameEventSystem
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EventIDAttribute : PropertyAttribute{}
}