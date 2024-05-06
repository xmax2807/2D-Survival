#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace Project.GameEventSystem.EventGraph.Editor
{
    public class EventGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        public event Action<SearchTreeEntry, SearchWindowContext> selectEntryEvent;
        private List<SearchTreeEntry> m_entries;
        public void ChangeSearchEntries(List<SearchTreeEntry> newEntries){
            m_entries = newEntries;
        }
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) => m_entries;

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context){
            selectEntryEvent?.Invoke(SearchTreeEntry, context);
            return true;
        }
    }
}
#endif