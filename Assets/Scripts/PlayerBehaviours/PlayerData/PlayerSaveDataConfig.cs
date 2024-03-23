using Project.SaveSystem;
using UnityEngine;

namespace Project.PlayerBehaviour
{
    [CreateAssetMenu(fileName = "PlayerSaveDataConfig", menuName = "SaveSystem/SaveDataConfig/PlayerData")]
    public class PlayerSaveDataConfig : SaveDataConfig
    {
        public override void AddNeedSaveDataTo(SaveSystemConfiguration saveSystemConfig)
        {
            saveSystemConfig.RegisterSaveableData<PlayerData>(MakeDefault);
            saveSystemConfig.RegisterSaveableData<PlayerInventoryData>(MakeDefaultInventory);
        }

        PlayerData MakeDefault()=> PlayerData.Randomize();
        PlayerInventoryData MakeDefaultInventory()=> new PlayerInventoryData(gold: 0);
    }
}