using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameFlowSystem
{
    [CreateAssetMenu(menuName = "GameFlowSystem/CommandProvider", fileName = "CommandProvider")]
    public class DefaultCommandProvider : CommandProvider
    {
        [System.Serializable]
        public struct CommandMapId{
            public CommandType CommandType;
            public int Id;
        }
        [SerializeField] private CommandMapId[] m_commandMapIds;
        [SerializeField] GameStateCommand.StateCommandProvider m_stateCommandProvider;
        public Dictionary<CommandType, int> m_commandMap;

        void OnEnable(){
            if(m_commandMapIds == null) return;

            m_commandMap ??= new Dictionary<CommandType, int>();
            for(int i = 0; i < m_commandMapIds.Length; ++i){
                m_commandMap.Add(m_commandMapIds[i].CommandType, m_commandMapIds[i].Id);
            }
        }
        void OnDisable(){
            m_commandMap?.Clear();
        }

        public override IGameStateCommand GetCommand(CommandType commandType)
        {
            if(m_commandMap.ContainsKey(commandType)){
                var command = m_stateCommandProvider.GetCommand(m_commandMap[commandType]);
                return new GameStateCommandAdapter(command);
            }

            throw new System.NotImplementedException($"Command with type {commandType} not found");
        }



        public class GameStateCommandAdapter : IGameStateCommand
        {
            Project.GameStateCommand.IGameStateCommand m_command;
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