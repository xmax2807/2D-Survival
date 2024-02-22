namespace Project.GameEventSystem
{
    public struct MaterialDetectionEventData
    {
        public Enums.MaterialDectectionType DectectionType;
        public Enums.FeedbackType FeedbackType;

        public MaterialDetectionEventData(Enums.MaterialDectectionType materialDectectionType, Enums.FeedbackType feedbackType){
            DectectionType = materialDectectionType;
            FeedbackType = feedbackType;
        }
    }
}