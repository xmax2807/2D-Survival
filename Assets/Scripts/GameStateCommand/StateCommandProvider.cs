#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
#endif

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Project.Utils.SerializeType;
namespace Project.GameStateCommand
{
    [CreateAssetMenu(fileName = "StateCommandProvider", menuName = "GameStateCommand/StateCommandProvider")]
    public class StateCommandProvider : ScriptableObject{
        [SerializeField, TypeFilter(typeof(IGameStateCommand))] private SerializeType[] _stateTypes;
        private readonly Dictionary<string, IGameStateCommand> m_stateCommands = new();

        public IGameStateCommand GetCommand(string name){
            if(!m_stateCommands.ContainsKey(name)){
                throw new System.Exception($"Command with name {name} not found");
            }
            return m_stateCommands[name];
        }

        public IGameStateCommand GetCommand(string name, params object[] parameters){
            if(!m_stateCommands.ContainsKey(name)){
                throw new System.Exception($"Command with name {name} not found");
            }

            return m_stateCommands[name].Clone(parameters);
        }

        void OnEnable(){
            foreach(SerializeType type in _stateTypes){
                if(type.Type == null){
                    continue;
                }

                m_stateCommands[type.Type.Name] = (IGameStateCommand)Activator.CreateInstance(type.Type);
            }
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if(_stateTypes == null || _stateTypes.Length == 0){
                _stateNames = new string[0];
                return; 
            }
            _stateNames = _stateTypes.Select(x =>{
                if(x.Type == null) return string.Empty;
                return x.Type.Name;
            }).ToArray();
        }
        internal static int FindIndex(string stringValue)
        {
            for(int i = 0; i < StateNames.Length; i++){
                if(StateNames[i] == stringValue){
                    return i;
                }
            }
            return -1;
        }

        private static string[] _stateNames;
        public static string[] StateNames {
            get{
                _stateNames ??= Assembly.GetAssembly(typeof(IGameStateCommand)).GetTypes().Where(x => typeof(IGameStateCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(x => x.Name).ToArray();
                return _stateNames;
            }
        }
        #endif
    } 
}