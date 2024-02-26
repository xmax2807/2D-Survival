using Project.Pooling;
using UnityEngine;

namespace Project.LootSystem
{
    [CreateAssetMenu(menuName = "LootSystem/LootTableSource/InProject", fileName = "LootTableSource")]
    public class InProjectLootTableSource : LootTableSource
    {
        #region Gold Config
        protected IPool<Gold> goldPool;
        [Header("Gold")]
        [SerializeField] private Gold goldPrefab;
        [SerializeField] private FeedbackData[] goldLootFeedbacks;
        private AutoLootableItemPreset goldAutoLootableItemPreset;
        protected override AutoLootableItemPreset GoldAutoLootableItemPreset
        {
            get
            {
                goldAutoLootableItemPreset ??= new AutoLootableItemPreset(goldPrefab, goldLootFeedbacks);
                return goldAutoLootableItemPreset;
            }
        }
        #endregion

        protected override AutoLootableItem GetGoldObject(int amount)
        {
            Gold gold = goldPool.Get();

            float value;
            if (amount >= 1000)
            {
                value = 2;
            }
            else if (amount >= 500)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            gold.gameObject.SetActive(true);
            gold.ChangeAnimParamValue("Type", value);
            return gold;
        }

        protected override void ReturnGoldObject(AutoLootableItem item)
        {
            if (item is Gold goldObject)
            {
                goldObject.gameObject.SetActive(false);
                goldPool.Return(goldObject);
            }
        }

        public override void Initialize(Transform container)
        {
            base.Initialize(container);
            goldPool = new CustomCreationPool<Gold>(() =>
            {
                Gold gold = Instantiate(goldPrefab, container);
                gold.gameObject.SetActive(false);
                return gold;
            });
        }
    }
}