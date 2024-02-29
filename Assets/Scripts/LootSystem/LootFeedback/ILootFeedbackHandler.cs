using UnityEngine;

namespace Project.LootSystem
{

    /// <summary>
    /// primarily play feedback upon collected it
    /// </summary>
    public interface ILootFeedbackHandler
    {
        void PlayFeedback(FeedbackData[] data, Transform pickerTransform = null);
    }
}