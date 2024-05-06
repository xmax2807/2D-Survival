using System.Collections.Generic;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph
{
    public interface IEventTriggerSource{

    }
    public abstract class EventTriggerSource : MonoBehaviour
    {
        private List<EventNodeData> nodes;
        private List<EventLinkData> links;

        protected void Trigger(){}
    }
}