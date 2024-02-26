using System.IO;
using System.Threading;
using MessagePack;
using Project.MessagePack.Resolvers;
using UnityEngine;

namespace Project.SaveSystem
{
    public class TestSave : MonoBehaviour
    {
        private SaveSystem saveSystem;
        [SerializeField] bool isSave = true;
        [SerializeField] bool isLoad = false;

        void Awake(){
            saveSystem = new SaveSystem();
        }

        void Update(){
            if(isSave){
                saveSystem.Save();
                isSave = false;
            }
            if(isLoad){
                saveSystem.Load();
                isLoad = false;
            }
        }

        // void Start(){
        //     MPSerializer.Initialize();
        //     byte[] bytes = MessagePackSerializer.Serialize(GameData.Randomize());

        // }
    }
}