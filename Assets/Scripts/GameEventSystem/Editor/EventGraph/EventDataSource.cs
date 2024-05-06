#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public interface IEventDataSource{
        List<EventNodeData> Nodes { get; }
        List<EventLinkData> Links { get; }
        EventNodeData this[Guid id] { get; }

        string AssetPath {get;}

        void AddNode(EventNodeData data);
        void AddLink(EventLinkData linkData);
        void RemoveNode(Guid id);
        void RemoveLink(string portName, Guid fromNodeId, Guid toNodeId);
        EventGraphConfig Config { get; }
        INodeDataCreator NodeDataCreator { get; }
        public void SaveChange();
    }
    public abstract class EventDataSource : ScriptableObject, IEventDataSource
    {
        public List<EventNodeData> Nodes = new();
        public List<EventLinkData> Links = new();

        public void OnDisable(){
            _config.OnDisable();
        }

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

        [SerializeField]private ScriptableGraphConfig _config;

        List<EventNodeData> IEventDataSource.Nodes => Nodes;

        List<EventLinkData> IEventDataSource.Links => Links;

        public EventGraphConfig Config => _config.GetConfig();

        public string AssetPath => AssetDatabase.GetAssetPath(this);

        public abstract INodeDataCreator NodeDataCreator {get;}

        public EventNodeData this[Guid id] => GetNodeData(id);

        private EventNodeData GetNodeData(Guid id)
        {
            foreach (var node in Nodes)
            {
                if (node.Id == id)
                {
                    return node;
                }
            }
            return null;
        }

        public void SaveChange()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
    }
}
#endif