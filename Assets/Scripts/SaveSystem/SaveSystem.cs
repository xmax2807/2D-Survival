using System;

namespace Project.SaveSystem
{
    public class SaveSystem
    {
        readonly SaveSystemConfiguration configuration;

        public event Action<GameData> SaveGameLoadedEvent;
        public event Action<bool> SaveGameSavedEvent;
        GameData data;

        string CurrentFileName => configuration.CurrentFileName;
        IDataService FileDataService => configuration.DataService;
        private SerializableGameData serializableSaveables;

        public SaveSystem(SaveSystemConfiguration config)
        {
            this.configuration = config;
        }

        public void NewGame(){
            data = configuration.RequestNewData();
            serializableSaveables = new SerializableGameData();
        }

        public async void Save()
        {
            if(data == null){
                NewGame();
            }
            serializableSaveables.SetDataToNonAlloc(data.SaveablesDict);
            bool result = await FileDataService.SaveAsync(CurrentFileName, serializableSaveables);
            SaveGameSavedEvent?.Invoke(result);
        }

        public async void Load()
        {
            if(data == null){
                NewGame();
            }
            serializableSaveables = await FileDataService.LoadAsync<SerializableGameData>(CurrentFileName);
            serializableSaveables.SetDataToNonAlloc(data.SaveablesDict);
            //UnityEngine.Debug.Log(data);
            SaveGameLoadedEvent?.Invoke(data);
        }
    }
    
}