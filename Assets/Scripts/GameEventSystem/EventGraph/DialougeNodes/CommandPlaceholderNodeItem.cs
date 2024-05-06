using UnityEngine;
using Project.GameEventSystem.Editor;
using Codice.CM.Common;

namespace Project.GameEventSystem.EventGraph
{
    [System.Serializable]
    public class CommandPlaceholderNodeItem
    {
        public int commandId => commandHolder.commandId;
        [SerializeField] public bool HasCondition;
        [SerializeField] public CommandConditionHolder[] conditions;
        [SerializeField] private CommandHolder commandHolder;
        public Parameters8 Parameters => commandHolder.Parameters;

        #if UNITY_EDITOR
        public CommandPlaceholderNodeItem(int id, string name)
        {
            commandHolder = new CommandHolder(id, name);
        }
        #endif
    }

    [System.Serializable]
    public class ConditionOutputPort{
        [UnityEngine.SerializeField] string PortName; // command name
        [UnityEngine.SerializeField] int CommandId;

        public ConditionOutputPort(string commandName, int commandId){
            PortName = commandName;
            CommandId = commandId;
        }

        public override string ToString()
        {
            return $"{CommandId} - {PortName}";
        }
    }

    [System.Serializable]
    public class ConditionPlaceholderNodeItem{
        [SerializeField] CommandConditionHolder commandHolder;
        [SerializeField] ConditionOutputPort outputPort;

        #if UNITY_EDITOR
        public ConditionPlaceholderNodeItem(int id, string name){
            commandHolder = new CommandConditionHolder(id, name);
            outputPort = new ConditionOutputPort(name, id);
        }
        #endif
    }

    [System.Serializable]
    public class CommandConditionHolder{
        [SerializeField] CommandHolder commandHolder;
        [SerializeField] int expectedResult;

        public int CommandId => commandHolder.commandId;
        public Parameters8 Parameters => commandHolder.Parameters;

        #if UNITY_EDITOR
        public CommandConditionHolder(int id, string name){
            commandHolder = new CommandHolder(id, name);
        }
        #endif
    }

    [System.Serializable]
    public class CommandHolder{
        [ReadOnly] public int commandId;
        #if UNITY_EDITOR
        [ReadOnly] public string commandName;
        #endif

        [SerializeField] private Parameters8 parameters;
        public Parameters8 Parameters => parameters;

        #if UNITY_EDITOR
        public CommandHolder(int id, string name)
        {
            this.commandId = id;
            this.commandName = name;
        }
        #endif
    }
}