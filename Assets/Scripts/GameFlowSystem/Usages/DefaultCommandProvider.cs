using System.Collections;
using System.Collections.Generic;
using Project.GameStateCommand;
using UnityEngine;

namespace Project.GameFlowSystem.InProject
{
    [CreateAssetMenu(menuName = "GameFlowSystem/InProject/CommandProvider", fileName = "CommandProvider")]
    public class DefaultCommandProvider : CommandProvider
    {
        [System.Serializable]
        public class CommandMapId{
            public CommandType CommandType;
            [SerializeField,CommandName]public string CommandName;
        }
        [SerializeField] private CommandMapId[] m_commandMapIds;
        [SerializeField] GameStateCommand.StateCommandProvider m_stateCommandProvider;
        public Dictionary<CommandType, CommandMapId> m_commandMap;

        void Init(){
            if(m_commandMapIds == null) return;

            m_commandMap ??= new Dictionary<CommandType, CommandMapId>();
            for(int i = 0; i < m_commandMapIds.Length; ++i){
                m_commandMap.Add(m_commandMapIds[i].CommandType, m_commandMapIds[i]);
            }
        }
        void OnDisable(){
            m_commandMap?.Clear();
        }

        public override IGameStateCommand GetCommand(CommandType commandType)
        {
            if(m_commandMap == null){
                Init();
            }
            if(m_commandMap.ContainsKey(commandType)){
                var command = m_stateCommandProvider.GetCommand(m_commandMap[commandType].CommandName);
                return new GameStateCommandAdapter(command);
            }

            throw new System.NotImplementedException($"Command with type {commandType} not found");
        }

        public class GameStateCommandAdapter : IGameStateCommand
        {
            readonly Project.GameStateCommand.IGameStateCommand m_command;
            public GameStateCommandAdapter(Project.GameStateCommand.IGameStateCommand command){
                m_command = command;
            }
            public IEnumerator GetTask()
            {
                return m_command.Execute();
            }
        }
    }
}