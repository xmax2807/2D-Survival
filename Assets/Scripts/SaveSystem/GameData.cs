using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace Project.SaveSystem
{
    [MessagePackObject]
    public class SerializableGameData : SerializableDictionary<string, ISaveable>
    {
        public SerializableGameData() : base() {}
        public SerializableGameData(Dictionary<string, ISaveable> saveables) : base(saveables) { }
    }

    public class GameData
    {
        public readonly Dictionary<string, ISaveable> SaveablesDict;
        public ISaveable[] Saveables => SaveablesDict.Values.ToArray();
        public GameData(Dictionary<string, ISaveable> saveables){
            SaveablesDict = saveables;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var kvp in SaveablesDict){
                sb.Append(kvp.Value.ToString());
            }

            return sb.ToString();
        }
    }
}