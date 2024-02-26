using UnityEngine;

namespace Project.LootSystem
{
    public abstract class LootFeedbackBuilder : ScriptableObject
    {
        public abstract ILootFeedbackHandler BuildLootFeedbackHandler();
    }
}