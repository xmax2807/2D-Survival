using System;
using MessagePack;
using UnityEngine;

namespace Project.SaveSystem
{
    public class SaveSystem
    {
        bool isTesting = true;
        string testFileName = "test";
        string currentFileName;

        readonly BinaryFileDataService fileDataService;

        public SaveSystem(){
            fileDataService = new BinaryFileDataService(new MPSerializer(), Application.persistentDataPath);
        }

        public async void Save(){
            string fileName = isTesting ? testFileName : currentFileName;

            if(isTesting){
                await fileDataService.SaveAsync(fileName, GameData.Randomize());
            }
        }

        public async void Load(){
            string fileName = isTesting ? testFileName : currentFileName;
            if(isTesting){
                GameData data = await fileDataService.LoadAsync<GameData>(fileName);
                Debug.Log(data.ToString());
            }
        }
    }

    [System.Serializable, MessagePackObject(true)]
    public class GameData
    {
        public string testString;
        public int testInt;
        public float testFloat;
        public bool testBool;

        public static GameData Randomize()
        {
            return new GameData(){
                testString = Guid.NewGuid().ToString(),
                testInt = UnityEngine.Random.Range(0, 100),
                testFloat = UnityEngine.Random.Range(0f, 1f),
                testBool = UnityEngine.Random.Range(0, 10) > 5
            };
        }

        public override string ToString() => $"string: {testString}, int: {testInt}, float: {testFloat}, bool: {testBool}";
    }
}