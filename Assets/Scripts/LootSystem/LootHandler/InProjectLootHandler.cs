using System;
using System.Collections;
using System.Collections.Generic;
using Project.SpawnSystem;
using Project.Utils;
using UnityEngine;

namespace Project.LootSystem
{
    public class InProjectLootHandler : ILootHandler
    {
        public event Action<IReadOnlyAutoLootContext, Transform> OnLootComplete;

        readonly Dictionary<int, IReadOnlyAutoLootContext> m_outcome;
        readonly ScriptableRewardableEntityRegistry m_rewardableEntityRegistry;

        public InProjectLootHandler(ScriptableRewardableEntityRegistry rewardableEntityRegistry){
            m_outcome = new();
            m_rewardableEntityRegistry = rewardableEntityRegistry;
        }
        public void HandleAutoLoot(IReadOnlyAutoLootContext context)
        {
            m_outcome.Add(context.Item.Id, context);
            context.Item.OnPickerApproachedEvent += OnPickerApproach;
        }

        private void OnPickerApproach(IAutoLootableObject lootObj, GameObject picker)
        {
            lootObj.OnPickerApproachedEvent -= OnPickerApproach;
            //check if rewardable
            IRewardableEntity rewardableEntity = m_rewardableEntityRegistry.GetById(picker.GetInstanceID());
            if(rewardableEntity == null){
                return;
            }
            //simulate
            if(lootObj is IVisibleLootObject2D visibleLootObject2D){
                Coroutines.StartCoroutine(SimulateAutoLootObject(visibleLootObject2D, picker.transform, 
                onDone: ()=>GiveRewardTo(rewardableEntity, lootObj.Id)));
            }
            else{
                GiveRewardTo(rewardableEntity, lootObj.Id);
            }
        }

        private void GiveRewardTo(IRewardableEntity rewardableEntity, int lootObjId)
        {
            IReadOnlyAutoLootContext context = m_outcome[lootObjId];
            var reward = context.LootOutcome;

            switch(reward.OutcomeType){
                case OutcomeType.Gold: rewardableEntity.GiveGold(reward.GetValue<int>()); break;
                case OutcomeType.Exp: rewardableEntity.GiveExp(reward.GetValue<int>()); break;
                case OutcomeType.Item: rewardableEntity.GiveItems(reward.GetValue<int[]>()); break;
            }
            m_outcome.Remove(lootObjId);

            OnLootComplete?.Invoke(context, rewardableEntity.transform);
        }

        IEnumerator SimulateAutoLootObject(IVisibleLootObject2D lootObject, Transform picker, Action onDone){
            float speed = 2f * Time.deltaTime;
            Transform lootObjTransform = lootObject.transform;
            while(Vector2.SqrMagnitude(lootObjTransform.position - picker.position) > 0.1f){
                lootObjTransform.position = Vector2.MoveTowards(lootObjTransform.position, picker.position, speed);
                yield return null;
                speed *= 1 + Time.deltaTime;
            }

            onDone?.Invoke();
        }
    }
}