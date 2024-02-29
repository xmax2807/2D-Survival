using System;
using Project.Utils;
using UnityEngine;

namespace Project.SaveSystem
{
    [CreateAssetMenu(menuName = "SaveSystem/SaveDataConfig/Test", fileName = "TestSaveDataConfig")]
    public class TestSaveDataConfig : SaveDataConfig
    {
        public override void AddNeedSaveDataTo(SaveSystemConfiguration saveSystemConfig)
        {
            saveSystemConfig.RegisterSaveableData<TestSaveData>(MakeDefault);
        }

        private ISaveable MakeDefault()
        {
            return new TestSaveData();
        }
    }

    [Serializable, MessagePack.MessagePackObject(true)]
    public class TestSaveData : ISaveable
    {
        public SerializableGuid Id => SerializableGuid.NewGuid();

        public int TestInt {get; set;}
        public float TestFloat {get; set;}
        public string TestString {get; set;}
        public bool TestBool {get; set;}

        public override string ToString(){
            return $"Id: {Id}, TestInt: {TestInt}, TestFloat: {TestFloat}, TestString: {TestString}, TestBool: {TestBool}";
        }
    }
}