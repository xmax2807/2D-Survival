using UnityEngine;

namespace Project.SaveSystem
{
    public abstract class SaveDataConfig : ScriptableObject
    {
        public abstract void AddNeedSaveDataTo(SaveSystemConfiguration saveSystemConfig);
    }
}