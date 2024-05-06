namespace Project.GameEventSystem.EventGraph
{
    public class EventStateConditionNodeData : EventNodeData<CommandConditionHolder>
    {
        [UnityEngine.SerializeField] ConditionOutputPort[] m_outPorts;
        public CommandConditionHolder[] Items => m_items;
        #if UNITY_EDITOR
        public string GetPortName(int index) => m_outPorts[index].ToString();
        public void AddConditionAt(int index, int id, string commandName){
            UnityEngine.Debug.Log($"{m_items.Length} - {m_outPorts.Length} - {index}");
            m_items[index] = new CommandConditionHolder(id, commandName);
            m_outPorts[index] = new ConditionOutputPort(commandName, id);
            InvokeItemsChanged();
        }
        public string[] GetPortNames(){
            if(m_outPorts == null) return System.Array.Empty<string>();
            string[] names = new string[m_outPorts.Length];
            for(int i = 0; i < m_outPorts.Length; i++){
                names[i] = m_outPorts[i].ToString();
            }
            return names;
        }
        #endif
    }
}