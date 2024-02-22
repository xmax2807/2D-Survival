using UnityEngine;

namespace Project.GameEventSystem
{
    public struct MaterialDetectionEventData
    {
        public Enums.MaterialDectectionType DectectionType;
        public Enums.FeedbackType FeedbackType;
        public Vector2 AtPosition;

        public MaterialDetectionEventData(Enums.MaterialDectectionType materialDectectionType, Enums.FeedbackType feedbackType, Vector2 atPosition){
            DectectionType = materialDectectionType;
            FeedbackType = feedbackType;
            AtPosition = atPosition;
        }
    }
}