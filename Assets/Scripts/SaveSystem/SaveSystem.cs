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

        public SaveSystem(SaveSystemConfiguration config)
        {
            this.configuration = config;
        }

        public void NewGame(){
            data = configuration.RequestNewData();
        }

        public async void Save()
        {
            if(data == null){
                NewGame();
            }
            bool result = await FileDataService.SaveAsync(CurrentFileName, new SerializableGameData(data.SaveablesDict));
            SaveGameSavedEvent?.Invoke(result);
        }

        public async void Load()
        {
            var serializableSaveables = await FileDataService.LoadAsync<SerializableGameData>(CurrentFileName);
            if(serializableSaveables == null){ // failed in loading
                throw new Exception("Failed load data save at " + CurrentFileName);
            }
            data = serializableSaveables.Result;
            UnityEngine.Debug.Log(data.ToString());
            SaveGameLoadedEvent?.Invoke(data);
        }
    }
    
}