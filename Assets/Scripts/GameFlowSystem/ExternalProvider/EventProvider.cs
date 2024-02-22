using System;
using UnityEngine;

namespace Project.GameFlowSystem
{
    
    public abstract class EventProvider : ScriptableObject
    {
        public abstract void RegisterToGameEvent(GameSystemEventType eventType, Delegate callback);
        public abstract void UnregisterFromGameEvent(GameSystemEventType eventType, Delegate callback);
    }
}