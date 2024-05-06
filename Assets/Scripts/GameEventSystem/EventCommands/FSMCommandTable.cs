using System.Collections.Generic;
using UnityEngine;
namespace Project.GameEventSystem.EventCommand
{
    [CreateAssetMenu(fileName = "EventCommandTable", menuName = "GameEventSystem/EventCommandTable", order = 0)]
    public class FSMCommandTable : ScriptableObject, IEventCommandProvider
    {
        #region Ids
        [Header("void commands")]
        [SerializeField] private int m_testCommandId = 0;
        [SerializeField] private int m_addContentListId;
        [SerializeField] private int m_callMachineId;

        [Header("result commands")]
        [SerializeField] private int m_getEventStatusId;
        [SerializeField] private int m_comparePlayerInventoryId;
        #endregion

        #if UNITY_EDITOR
        readonly Dictionary<int, string> m_commandMap = new Dictionary<int, string>();
        void Init(){
            m_commandMap.Clear();
            m_commandMap.Add(m_testCommandId, "Test Command");
            m_commandMap.Add(m_addContentListId, "Add Content List Command");
            m_commandMap.Add(m_callMachineId, "Call Machine Command");
            m_commandMap.Add(m_getEventStatusId, "Get Event Status Command");
            m_commandMap.Add(m_comparePlayerInventoryId, "Compare Player Inventory Command");
        }

        public int GetCommandId(string commandName)
        {
            if(m_commandMap.Count == 0){
                Init();
            }
            //get key from value
            foreach(KeyValuePair<int, string> pair in m_commandMap){
                if(pair.Value == commandName){
                    return pair.Key;
                }
            }
            return -1;
        }

        public string GetCommandName(int commandId)
        {
            if(m_commandMap.Count == 0){
                Init();
            }
            if(m_commandMap.ContainsKey(commandId)){
                return m_commandMap[commandId];
            }
            return string.Empty;
        }

        private void OnValidate()
        {
            Init();
        }
        public string[] GetVoidCommandNames()
        {
            if(m_commandMap.Count == 0){
                Init();
            }
            return new string[]{
                m_commandMap[m_testCommandId], 
                m_commandMap[m_addContentListId], 
                m_commandMap[m_callMachineId]
            };
        }

        public string[] GetResultCommandNames(){
            if(m_commandMap.Count == 0){
                Init();
            }
            
            return new string[] {
                m_commandMap[m_getEventStatusId],
                m_commandMap[m_comparePlayerInventoryId]
            };
        }
        #endif
    }
}