#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Project.GameEventSystem.EventGraph.Editor
{
    public class StateMachineNodeCreator : INodeDataCreator
    {
         private FSMDataSource parentSource;
        private List<SearchTreeEntry> m_entries;

        public StateMachineNodeCreator(FSMDataSource parentSource)
        {
            this.parentSource = parentSource;
        }

        public List<SearchTreeEntry> GetSearchTreeEntries()
        {
            if(m_entries != null) return m_entries;
            
            m_entries =  new List<SearchTreeEntry>(){
                new SearchTreeGroupEntry(new GUIContent("State Machine Node")),
                new SearchTreeEntry(new GUIContent("New State Machine")){
                    userData = 0,
                    level = 1,
                },
            };

            return m_entries;
        }

        public void RemoveAsset(EventNodeData nodeData)
        {
            AssetDatabase.RemoveObjectFromAsset(nodeData);
            AssetDatabase.SaveAssets();
        }

        public EventNodeData CreateAsset(int nodeIndex, string location)
        {
            if(nodeIndex != 0){
                return null;
            }
            StateDataSource result = ScriptableObject.CreateInstance<StateDataSource>();
            result.name = "New State Machine";
            if(result == null){
                throw new System.NotImplementedException();
            }
            AssetDatabase.AddObjectToAsset(result, parentSource);

            result.Parent = parentSource;
            result.Name = "New State Machine";
            AssetDatabase.SaveAssets();
            return result;
        }

        private EventNodeData SaveAsset(EventNodeData asset, string location){
            //ensure last character is a '/'
            if(!location.EndsWith("/")){
                location += '/';
            }
            AssetDatabase.AddObjectToAsset(asset, parentSource);
            string path = AssetDatabase.GenerateUniqueAssetPath(string.Concat(location, asset.GetType().Name, ".asset"));
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            return AssetDatabase.LoadAssetAtPath<EventNodeData>(path);
        }
    }
    public class StateNodeDataCreator : INodeDataCreator
    {
        enum NodeType{
            Starter,
            StateCommand,
            StateCondition,
            End
        }
        private List<SearchTreeEntry> m_entries;

        public List<SearchTreeEntry> GetSearchTreeEntries()
        {
            if(m_entries != null) return m_entries;
            
            m_entries=  new List<SearchTreeEntry>(){
                new SearchTreeGroupEntry(new GUIContent("State Machine Node")),
                new SearchTreeEntry(new GUIContent("Start State")){
                    userData = NodeType.Starter,
                    level = 1
                },
                new SearchTreeEntry(new GUIContent("State Command")){
                    userData = NodeType.StateCommand,
                    level = 1
                },
                new SearchTreeEntry(new GUIContent("State Condition")){
                    userData = NodeType.StateCondition,
                    level = 1
                },
            };

            return m_entries;
        }

        public void RemoveAsset(EventNodeData nodeData)
        {
            string path = AssetDatabase.GetAssetPath(nodeData);
            if (false == AssetDatabase.DeleteAsset(path))
            {
                Debug.LogError($"Failed to delete asset at path: {path}");
            }
            AssetDatabase.SaveAssets();
        }

        public EventNodeData CreateAsset(int nodeIndex, string location)
        {
            EventNodeData result = CreateNodeData((NodeType)nodeIndex);
            if(result == null){
                throw new System.NotImplementedException();
            }
            return SaveAsset(result, location);
        }

        private EventNodeData CreateNodeData(NodeType nodeIndex)
        {
            EventNodeData result = null;
            switch (nodeIndex)
            {
                case NodeType.Starter:
                    //TODO: result = ScriptableObject.CreateInstance<EventStartNodeData>();
                    break;
                case NodeType.StateCommand:
                    result = ScriptableObject.CreateInstance<EventStateCommandNodeData>();
                    break;
                case NodeType.StateCondition:
                    result = ScriptableObject.CreateInstance<EventStateConditionNodeData>();
                    break;
                case NodeType.End:
                    //TODO: result = ScriptableObject.CreateInstance<EventEndNodeData>();
                    break;
                default:
                    break;
            }
            return result;
        }

        private EventNodeData SaveAsset(EventNodeData asset, string location){
            //ensure last character is a '/'
            if(!location.EndsWith("/")){
                location += '/';
            }
            string path = AssetDatabase.GenerateUniqueAssetPath(string.Concat(location, asset.GetType().Name, ".asset"));
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            return AssetDatabase.LoadAssetAtPath<EventNodeData>(path);
        }
    }
}
#endif