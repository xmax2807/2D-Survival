using UnityEngine;
namespace Project.LootSystem
{
    public interface ILootSystemAPI{
        void DropGold(int amount, Vector2 AtPosition);
        void DropExp(int amount, Vector2 AtPosition);
        void DropItems(int[] ids, Vector2 AtPosition);
    }
    
    public class LootSystem : ILootSystemAPI
    {
        LootTableSource m_LootTableSource;
        ILootHandler m_LootHandler;
        ILootFeedbackHandler m_LootFeedbackHandler;

        readonly Pooling.AutoExpandPoolNew<AutoLootContext> m_LootOutcomeContextPool;

        public LootSystem(){
            m_LootOutcomeContextPool = new Pooling.AutoExpandPoolNew<AutoLootContext>();
        }

        public void AddTableSource(LootTableSource lootTableSource){
            m_LootTableSource = lootTableSource;
        }
        internal void AddLootHandler(ILootHandler lootHandler){
            if(m_LootHandler != null){
                m_LootHandler.OnLootComplete -= OnLootComplete;
            }
            if(lootHandler != null){
                lootHandler.OnLootComplete += OnLootComplete;
            }
            m_LootHandler = lootHandler;
        }
        public void AddLootFeedbackHandler(ILootFeedbackHandler lootFeedbackHandler) => m_LootFeedbackHandler = lootFeedbackHandler;

        #region ILootSystemAPI
        public void DropExp(int amount, Vector2 AtPosition)
        {
            throw new System.NotImplementedException();
        }

        public void DropGold(int amount, Vector2 AtPosition)
        {
            AutoLootableItem gold = m_LootTableSource.GetAutoLootableItem(AutoLootableItemType.Gold,amount);
            gold.transform.position = AtPosition;
            
            AutoLootContext context = m_LootOutcomeContextPool.Get();
            context.LootOutcome = new LootOutcome(OutcomeType.Gold, amount);
            context.Item = gold;
            context.ItemType = AutoLootableItemType.Gold;

            m_LootHandler.HandleAutoLoot(context);   
        }

        private void OnLootComplete(IReadOnlyAutoLootContext context, Transform pickerTransform)
        {
            AutoLootContext realContext = (AutoLootContext)context;
            if(context == null){
                #if UNITY_EDITOR
                Debug.LogWarning($"{context.GetType()} is not AutoLootContext");
                #endif
                return;
            }
            m_LootTableSource.ReturnAutoLootableItem(realContext.ItemType, realContext.GetItem<AutoLootableItem>());
            FeedbackData[] data = m_LootTableSource.GetAutoLootableItemFeedbacks(realContext.ItemType);
            m_LootFeedbackHandler.PlayFeedback(data);
            m_LootOutcomeContextPool.Return(realContext);
        }

        public void DropItems(int[] ids, Vector2 AtPosition)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}