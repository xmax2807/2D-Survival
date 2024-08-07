namespace Project.AnimationEventSystem
{
    internal class MaterialDetectionEventData : AnimationEventData
    {
        public MaterialDectectionType MaterialDectectionType { get; private set; }
        public ActionType ActionType { get; private set; }

        public MaterialDetectionEventData() { }
        protected MaterialDetectionEventData(MaterialDectectionType materialDectectionType, ActionType actionType)
        {
            ActionType = actionType;
            MaterialDectectionType = materialDectectionType;
        }

        public override AnimationEventData Clone()
        {
            return new MaterialDetectionEventData(MaterialDectectionType, ActionType);
        }

        public override void MapFromOther(AnimationEventData other)
        {
            if (other is MaterialDetectionEventData materialDetectionEventData){
                MaterialDectectionType = materialDetectionEventData.MaterialDectectionType;
                ActionType = materialDetectionEventData.ActionType;
            }
        }

        protected override void MapFromStrings(string[] data)
        {
            MaterialDectectionType = (MaterialDectectionType)int.Parse(data[0]);
            ActionType = (ActionType)int.Parse(data[1]);
        }
    }
}