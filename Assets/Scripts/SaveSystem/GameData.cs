using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Project.SaveSystem
{
    [MessagePackObject]
    public class SerializableGameData : SerializableDictionary<System.Type, ISaveable>
    {
        public SerializableGameData() : base() {}
        public SerializableGameData(Dictionary<System.Type, ISaveable> saveables) : base(saveables) { }

        public void SetDataToNonAlloc(in Dictionary<Type, ISaveable> dictionary){
            for(int i = keys.Count - 1; i >= 0; --i){
                if(!dictionary.ContainsKey(keys[i])){
                    continue;
                }

                dictionary[keys[i]] = values[i];
            }
        }
    }

    public class GameData
    {
        public Dictionary<System.Type, ISaveable> SaveablesDict { get; private set; }
        public ISaveable[] Saveables => SaveablesDict.Values.ToArray();
        public GameData(Dictionary<System.Type, ISaveable> saveables){
            SaveablesDict = saveables;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new();
            foreach (var kvp in SaveablesDict){
                sb.Append(kvp.Value.ToString());
            }

            return sb.ToString();
        }

        internal void Set(Dictionary<Type, ISaveable> dictionary)
        {
            foreach (var kvp in dictionary){
                if(!SaveablesDict.ContainsKey(kvp.Key)){
                    continue;
                }
                SaveablesDict[kvp.Key] = kvp.Value;
            }
        }
    }
}