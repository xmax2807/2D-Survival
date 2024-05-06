using System;
using UnityEngine;
namespace Project.GameEventSystem.EventGraph
{
    [Serializable]
    public class EventLinkData : ISerializationCallbackReceiver, IEquatable<EventLinkData>
    {
        public Guid OutputId; // id of node that has output port containing this link
        public Guid InputId; // id of node that has input port containing this link
        public string PortName; // output port name

        [SerializeField] private string outputIdString;
        [SerializeField] private string inputIdString;

        public bool Equals(EventLinkData other)
        {
            return OutputId == other.OutputId && InputId == other.InputId && PortName == other.PortName;
        }

        public void OnAfterDeserialize()
        {
            Guid.TryParse(outputIdString, out OutputId);
            Guid.TryParse(inputIdString, out InputId);
        }

        public void OnBeforeSerialize()
        {
            outputIdString = OutputId.ToString();
            inputIdString = InputId.ToString();
        }

        override public string ToString()
        {
            return $"[EventLinkData: {OutputId} -> {InputId} ({PortName})]";
        }

#if UNITY_EDITOR
        public void SetInputId(Guid id)
        {
            InputId = id;
            OnBeforeSerialize();
        }
        public void SetOutputId(Guid id)
        {
            OutputId = id;
            OnBeforeSerialize();
        }
        public static EventLinkData CreateInEditor(Guid outputId, Guid inputId, string portName)
        {
            var result = new EventLinkData()
            {
                OutputId = outputId,
                InputId = inputId,
                PortName = portName
            };
            result.OnBeforeSerialize();
            return result;
        }

#endif
    }
}