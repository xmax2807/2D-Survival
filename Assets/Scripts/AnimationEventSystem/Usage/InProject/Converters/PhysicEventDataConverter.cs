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
            Enums.MaterialDectectionType detectionType;
            switch(data.MaterialDectectionType){
                case MaterialDectectionType.Floor: detectionType = Enums.MaterialDectectionType.Floor; break;
                case MaterialDectectionType.Body: detectionType = Enums.MaterialDectectionType.Body; break;
                case MaterialDectectionType.Wall: detectionType = Enums.MaterialDectectionType.Wall; break;
                default: detectionType = Project.Enums.MaterialDectectionType.Floor; break;
            }

            Enums.FeedbackType feedbackType;
            switch(data.ActionType){
                case ActionType.PlaySound: feedbackType = Enums.FeedbackType.PlaySound; break;
                case ActionType.PlayParticle: feedbackType = Enums.FeedbackType.PlayParticle; break;
                default: feedbackType = Enums.FeedbackType.None; break;
            }

            UnityEngine.Vector2 position = data.Invoker.transform.position;
            return new GameEventSystem.MaterialDetectionEventData(detectionType, feedbackType, position);
        }
    }
}