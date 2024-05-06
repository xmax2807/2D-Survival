#if UNITY_EDITOR
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
namespace Project.GameEventSystem.EventGraph.Editor{
    public class EventNode : Node{
        public Guid id;
    }
}
#endif