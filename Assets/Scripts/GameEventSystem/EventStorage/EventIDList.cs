using UnityEngine;

namespace Project.GameEventSystem
{
    [System.Serializable]
    public class EventIDList
    {
        [SerializeField, EventID] public int id_GameStartEvent;
        [SerializeField, EventID] public int id_GameEndEvent;
        [SerializeField, EventID] public int id_SplashScreenCompleted;
        [SerializeField, EventID] public int id_PlayerChanged;
        [SerializeField, EventID] public int id_PlaySoundEvent;
        [SerializeField, EventID] public int id_PlayVisualEffectEvent;
        [SerializeField, EventID] public int id_MaterialDetectionEvent;
        [SerializeField, EventID] public int id_InventoryItemAdded;
        [SerializeField, EventID] public int id_InventoryItemRemoved;
        [SerializeField, EventID] public int id_DropGoldEvent;
        [SerializeField, EventID] public int id_DropEXPEvent;
        [SerializeField, EventID] public int id_DropItemEvent;

        public void Map(int[] ids){
            id_GameStartEvent = ids[0];
            id_GameEndEvent = ids[1];
            id_SplashScreenCompleted = ids[2];
            id_PlayerChanged = ids[3];
            id_PlaySoundEvent = ids[4];
            id_PlayVisualEffectEvent = ids[5];
            id_MaterialDetectionEvent = ids[6];
            id_InventoryItemAdded = ids[7];
            id_InventoryItemRemoved = ids[8];
            id_DropGoldEvent = ids[9];
            id_DropEXPEvent = ids[10];
            id_DropItemEvent = ids[11];
        }
    }
}