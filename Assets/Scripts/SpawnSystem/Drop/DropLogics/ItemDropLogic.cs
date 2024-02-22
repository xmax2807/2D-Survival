using System.Collections.Generic;
using Project.Pooling;
using UnityEngine;

namespace Project.SpawnSystem
{
    public interface IItemDropLogic
    {
        void Drop(DropItem[] data, IRewardableEntity target);
    }

    public class InstantItemDropLogic : IItemDropLogic{
        public void Drop(DropItem[] items, IRewardableEntity target){
            float dropBonus = target.GetDropBonus();
            int chance = UnityEngine.Random.Range(0, 100);
            
            //Sort before drop
            System.Array.Sort(items, (a, b) => b.rate.CompareTo(a.rate));
            List<uint> listItemIds = QuickListPool<uint>.GetList();
            for(int i = 0; i < items.Length; ++i){
                //Calculate the drop amount
                if(chance > (int)(items[i].rate + dropBonus)){
                    continue;
                }
                else{
                    listItemIds.Add(items[i].ItemId);
                }
            }
            target.GiveItems(listItemIds.ToArray());
            QuickListPool<uint>.ReturnList(listItemIds);
        }
    }
}