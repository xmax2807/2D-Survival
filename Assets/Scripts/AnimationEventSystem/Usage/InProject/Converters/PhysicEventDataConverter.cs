namespace Project.AnimationEventSystem.UsageInProject
{
    public class PhysicEventDataConverter : IEventDataConverter
    {
        public object Convert(AnimationEventData data)
        {
            if(data is MaterialDetectionEventData materialEventData)
            {
                return ConvertToMaterialDetectionEventData(materialEventData);
            }

            throw new System.NotImplementedException($"can't convert {data.GetType().Name} into any data");
        }

        private object ConvertToMaterialDetectionEventData(MaterialDetectionEventData data){
            Project.Enums.MaterialDectectionType detectionType;
            switch(data.MaterialDectectionType){
                case MaterialDectectionType.Floor: detectionType = Project.Enums.MaterialDectectionType.Floor; break;
                case MaterialDectectionType.Body: detectionType = Project.Enums.MaterialDectectionType.Body; break;
                case MaterialDectectionType.Wall: detectionType = Project.Enums.MaterialDectectionType.Wall; break;
                default: detectionType = Project.Enums.MaterialDectectionType.Floor; break;
            }

            Project.Enums.FeedbackType feedbackType;
            switch(data.ActionType){
                case ActionType.PlaySound: feedbackType = Project.Enums.FeedbackType.PlaySound; break;
                case ActionType.PlayParticle: feedbackType = Project.Enums.FeedbackType.PlayParticle; break;
                default: feedbackType = Project.Enums.FeedbackType.None; break;
            }
            return new Project.GameEventSystem.MaterialDetectionEventData(detectionType, feedbackType);
        }
    }
}