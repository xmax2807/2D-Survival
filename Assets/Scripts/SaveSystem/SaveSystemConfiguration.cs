using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Project.SaveSystem
{
    [CreateAssetMenu(fileName = "SaveSystemConfiguration", menuName = "SaveSystem/SaveSystemConfiguration")]
    public class SaveSystemConfiguration : ScriptableObject
    {
        [SerializeField] SaveDataConfig[] saveDataConfigs;
        [SerializeField] ScriptableBindRegistry bindRegistry;
        Dictionary<Type, Func<ISaveable>> saveables;
        private SaveSystem saveSystem;
        public IDataService DataService { get; private set; }
        public string CurrentFileName { get; private set; }

        private void Initialize()
        {
            for (int i = 0; i < saveDataConfigs.Length; ++i)
            {
                saveDataConfigs[i].AddNeedSaveDataTo(this);
            }

            DataService = new BinaryFileDataService(serializer: new MPSerializer(), rootPath: Application.persistentDataPath);
            
            saveSystem = new SaveSystem(this);
            saveSystem.SaveGameLoadedEvent += OnGameLoaded;
            bindRegistry.Initialize(this.saveables.Keys.ToArray());
        }

        private void OnGameLoaded(GameData data)
        {
            bindRegistry.BindAllToRegistered(data.Saveables);
        }

        public SaveSystem GetSaveSystem()
        {
            if (saveSystem == null)
            {
                Initialize();
            }
            return saveSystem;
        }


        /// <summary>
        /// register data should be saved
        /// </summary>
        /// <param name="creator">default data when not found</param>
        /// <typeparam name="TSaveable"></typeparam>
        public void RegisterSaveableData<TSaveable>(Func<ISaveable> creator) where TSaveable : ISaveable
        {
            RegisterSaveableData(typeof(TSaveable), creator);
        }

        public void RegisterSaveableData(Type type, Func<ISaveable> creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException(nameof(creator));
            }

            saveables ??= new Dictionary<Type, Func<ISaveable>>();

            if (!saveables.ContainsKey(type))
            {
                saveables.Add(type, creator);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Data {type} already registered");
#endif
            }
        }

        public GameData RequestNewData()
        {
            Dictionary<Type, ISaveable> saveables = new();
            foreach (var saveable in this.saveables)
            {
                saveables.Add(saveable.Key, saveable.Value.Invoke());
            }

            return new GameData(saveables);
        }
    }
}