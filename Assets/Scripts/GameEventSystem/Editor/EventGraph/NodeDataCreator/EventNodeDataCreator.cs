#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Project.GameEventSystem.EventGraph.Editor
{
    public class ScriptableEventNodeDataCreator : INodeDataCreator{
        public readonly string saveLocation;
        public ScriptableEventNodeDataCreator(string saveLocation){
            this.saveLocation = saveLocation;
        }

        public event Action<EventNodeData> NodeCreateEvent;

        public EventNodeData CreateNodeData(int index, string saveLocation){
            EventNodeData result = null;
            switch(index){
                case 0:
                    result = SaveAsset(ScriptableObject.CreateInstance<EventConditionNodeData>(), saveLocation);
                    break;
                case 1:
                    result = SaveAsset(ScriptableObject.CreateInstance<EventReactionNodeData>(), saveLocation);
                    break;
                case 2:
                    result = SaveAsset(ScriptableObject.CreateInstance<EventStateCommandNodeData>(), saveLocation);
                    break;
                default:
                    break;
            }
            return result;
        }

        public List<SearchTreeEntry> GetSearchTreeEntries()
        {
            List<SearchTreeEntry> result = new()
            {
                new SearchTreeGroupEntry(new GUIContent("Condition")),
                new SearchTreeEntry(new GUIContent("Condition")){
                    userData = 0,
                    level = 1
                },
                new SearchTreeEntry(new GUIContent("Reaction")){
                    userData = 1,
                    level = 1
                },
                new SearchTreeEntry(new GUIContent("State Command")){
                    userData = 2,
                    level = 1
                }
            };
            return result;
        }

        private EventNodeData SaveAsset(EventNodeData asset, string location){
            //ensure last character is a '/'
            if(!location.EndsWith("/")){
                location += '/';
            }
            string path = AssetDatabase.GenerateUniqueAssetPath(string.Concat(location, "/", asset.GetType().Name, ".asset"));
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            return AssetDatabase.LoadAssetAtPath<EventNodeData>(path);
        }

        public void RemoveAsset(EventNodeData asset){
            string path = AssetDatabase.GetAssetPath(asset);
            if(false == AssetDatabase.DeleteAsset(path)){
                Debug.LogError($"Failed to delete asset at path: {path}");
            }
            AssetDatabase.SaveAssets();
        }

        public EventNodeData CreateAsset(int nodeIndex, string location)
        {
            return CreateNodeData(nodeIndex, location);
        }
    }
}
#endif