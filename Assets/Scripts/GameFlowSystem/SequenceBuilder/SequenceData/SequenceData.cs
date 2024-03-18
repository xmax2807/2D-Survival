using UnityEngine;

namespace Project.GameFlowSystem
{
    /// <summary>
    /// contains all the data needed to build a sequence
    /// </summary>
    [System.Serializable]
    public class SequenceData
    {
        [SerializeField]public CommandType[] commandTypes;
    }

    [System.Serializable]
    public class SequenceLinkData
    {
        public enum LinkType{
            Default,
            Event
        }

        [SerializeField]public LinkType linkType;
        [SerializeField]public GameSystemEventType eventType;
    }
}