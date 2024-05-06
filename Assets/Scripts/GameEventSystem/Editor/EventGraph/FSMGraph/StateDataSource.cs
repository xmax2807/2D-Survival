#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Project.GameEventSystem.Editor;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph.Editor{
    [System.Serializable]
    public class StateDataSource : EventNodeData, IEventDataSource
    {
        [SerializeField, ReadOnly] public FSMDataSource Parent;
        [SerializeField, ReadOnly] public string Name;
        public EventNodeData this[Guid id] => GetNodeData(id);
        public int StateCount => Nodes == null ? 0 : Nodes.Count;

        [SerializeField] private List<EventNodeData> Nodes = new List<EventNodeData>();
        [SerializeField] private List<EventLinkData> Links = new List<EventLinkData>();
        List<EventNodeData> IEventDataSource.Nodes => Nodes;
        List<EventLinkData> IEventDataSource.Links => Links;

        public string AssetPath => Parent.AssetPath;
        public void SaveChange() => Parent.SaveChange();
        public EventGraphConfig Config => Parent.Config;
        public INodeDataCreator NodeDataCreator => Parent.ChildNodeDataCreator;

        public void AddLink(EventLinkData linkData)
        {
            if (!Links.Contains(linkData))
            {
                Links.Add(linkData);
            }
        }

        public void RemoveNode(Guid id)
        {
            foreach (var node in Nodes)
            {
                if (node.Id == id)
                {
                    Nodes.Remove(node);
                    RemoveLinksFrom(id);
                    break;
                }
            }
        }

        public void RemoveLink(string portName, Guid fromNodeId, Guid toNodeId){
            for (int i = Links.Count - 1; i >= 0; --i)
            {
                if (Links[i].PortName != portName || Links[i].InputId != fromNodeId || Links[i].OutputId != toNodeId) continue;
                Links.RemoveAt(i);
                break;
            }
        }

        private void RemoveLinksFrom(Guid nodeId)
        {
            for (int i = Links.Count - 1; i >= 0; --i)
            {
                if (Links[i].InputId != nodeId || Links[i].OutputId != nodeId) continue;
                Links.RemoveAt(i);
            }
        }

        public void AddNode(EventNodeData data)
        {
            if(!Nodes.Contains(data))
            {
                Nodes.Add(data);
            }
        }
        
        private EventNodeData GetNodeData(Guid id)
        {
            foreach (var node in Nodes){
                if (node.Id == id){
                    return node;
                }
            }
            return null;
        }
    }
}
#endif