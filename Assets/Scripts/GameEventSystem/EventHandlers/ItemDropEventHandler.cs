using System;

namespace Project.GameEventSystem
{
    public class ItemDropEventHandler : EventHandler
    {
        readonly Action<int> GoldDropCallback;
        readonly Action<int> ExpDropCallback;
        readonly Action<int[]> ItemDropCallback;
        public ItemDropEventHandler(IEventAPI eventAPI) : base(eventAPI)
        {
            GoldDropCallback = OnGoldDrop;
            ExpDropCallback = OnExpDrop;
            ItemDropCallback = OnItemDrop;
        }

        public override void RegisterToAPI(){
            m_eventAPI.DropGoldEvent.Subscribe(GoldDropCallback);
            m_eventAPI.DropEXPEvent.Subscribe(ExpDropCallback);
            m_eventAPI.DropItemEvent.Subscribe(ItemDropCallback);
        }

        public override void UnregisterFromAPI(){
            m_eventAPI.DropGoldEvent.Unsubscribe(GoldDropCallback);
            m_eventAPI.DropEXPEvent.Unsubscribe(ExpDropCallback);
            m_eventAPI.DropItemEvent.Unsubscribe(ItemDropCallback);
        }

        public void OnGoldDrop(int amount){
            //TODO: call loot system to drop gold
            //test
            UnityEngine.Debug.Log("Gold dropped: " + amount);
        }
        public void OnExpDrop(int amount){
            //TODO: call loot system to drop exp
            //test
            UnityEngine.Debug.Log("Exp dropped: " + amount);
        }
        public void OnItemDrop(int[] itemIds){
            //TODO: call loot system to drop items

            //test
            UnityEngine.Debug.Log("Item dropped: " + itemIds?.Length);
        }
    }
}