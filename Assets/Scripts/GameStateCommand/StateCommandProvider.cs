using System.Collections.Generic;
using UnityEngine;
namespace Project.GameStateCommand
{
    [CreateAssetMenu(fileName = "StateCommandProvider", menuName = "GameStateCommand/StateCommandProvider")]
    public class StateCommandProvider : ScriptableObject{
        private Dictionary<int, IGameStateCommand> m_stateCommands;

        public IGameStateCommand GetCommand(int id){
            return m_stateCommands[id];
        }

        public void AddCommand(int id, IGameStateCommand command){
            if(!m_stateCommands.ContainsKey(id)){
                m_stateCommands.Add(id, command);
            }
        }

        public void RemoveCommand(int id){
            if(m_stateCommands.ContainsKey(id)){
                m_stateCommands.Remove(id);
            }
        }
    } 
}