using Project.GameEventSystem;
using Project.SpawnSystem;
using UnityEngine;

namespace Project.LootSystem
{
    [CreateAssetMenu(menuName = "LootSystem/LootSystemConfiguration", fileName = "LootSystemConfiguration")]
    public class LootSystemConfiguration : ScriptableObject
    {
        [SerializeField] private LootTableSource m_LootTableSource;
        [SerializeField] private ScriptableRewardableEntityRegistry m_RewardableEntityRegistry;
        [SerializeField] private LootFeedbackBuilder feedbackBuilder;

        LootSystem m_LootSystem;
        public ILootSystemAPI LootSystem => m_LootSystem;
        public bool IsInitialized => m_LootSystem != null;


        public void Initialize(Transform container){
            if(m_LootSystem != null){
                return;
            }
            m_LootSystem = new LootSystem();

            m_LootTableSource.Initialize(container);
            m_LootSystem.AddTableSource(m_LootTableSource);
            
            m_LootSystem.AddLootHandler(new InProjectLootHandler(m_RewardableEntityRegistry));

            m_LootSystem.AddLootFeedbackHandler(feedbackBuilder.BuildLootFeedbackHandler());
        }
    }
}