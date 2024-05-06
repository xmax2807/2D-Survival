#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public interface INodeDataCreator
    {
        System.Collections.Generic.List<SearchTreeEntry> GetSearchTreeEntries();
        EventNodeData CreateAsset(int nodeIndex, string location);
        void RemoveAsset(EventNodeData nodeData);
    }
}
#endif