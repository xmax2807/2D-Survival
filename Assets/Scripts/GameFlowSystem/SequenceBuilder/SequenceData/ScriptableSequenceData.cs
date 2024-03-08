using UnityEngine;
namespace Project.GameFlowSystem
{
    /// <summary>
    /// contains all the data needed to build a sequence
    /// </summary>
    [CreateAssetMenu(fileName = "SequenceData", menuName = "FlowSystem/SequenceData", order = 1)]
    public class ScriptableSequenceData : ScriptableObject
    {
        public int Id => _sequenceData == null? -1 : _sequenceData.GetHashCode();
        [SerializeField] private string _builderId;
        public string BuilderId => _builderId;
        [SerializeField] private SequenceData _sequenceData;
        public SequenceData SequenceData => _sequenceData;
        [SerializeField] SequenceLink[] _links;
        public SequenceLink[] Links => _links;


        /// <summary>
        /// Link Data between two sequences
        /// </summary>
        [System.Serializable]
        public class SequenceLink{
            //TODO add link type
            [SerializeField] private SequenceLinkData linkData;
            public SequenceLinkData Data => linkData;
            [SerializeField] private ScriptableSequenceData linkSequence;
            public ScriptableSequenceData SequenceData => linkSequence;
        }
    }
}