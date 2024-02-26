using System;
using Project.LootSystem;
using Project.Manager;
using UnityEngine;

namespace Project.GameEventSystem
{
    public class ItemDropEventHandler : EventHandler
    {
        private ILootSystemAPI _lootAPI;
        ILootSystemAPI LootAPI => _lootAPI ??= GameManager.LootSystem;
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
            Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-5f, 5f));
            LootAPI.DropGold(amount, randomPos);
        }
        public void OnExpDrop(int amount){
            //TODO: call loot system to drop exp
            //test
        }
        public void OnItemDrop(int[] itemIds){
            //TODO: call loot system to drop items

            //test
        }
    }
}