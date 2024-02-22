namespace Project.AnimationEventSystem
{
    public abstract class AnimationEventData
    {
        public const string SEPARATOR = ",";
        private static AnimationEventData _empty;
        public static AnimationEventData Empty{
            get {
                if(_empty == null){
                    _empty = new EmptyAnimationEventData();
                }
                return _empty;
            }
        }

        protected AnimationEventData(){}

        public abstract AnimationEventData Clone();
        public abstract void MapFromOther(AnimationEventData other);

        public void MapFromString(string data){
            if(string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            { 
                #if UNITY_EDITOR
                UnityEngine.Debug.LogError("data is null or empty");
                #endif
                return;
            }
            string[] dataSplit = data.Split(SEPARATOR);
            MapFromStrings(dataSplit);
        }
        protected abstract void MapFromStrings(string[] data);
    }

    public class EmptyAnimationEventData : AnimationEventData
    {
        public override AnimationEventData Clone()
        {
            return new EmptyAnimationEventData();
        }

        public override void MapFromOther(AnimationEventData other)
        {
            // do nothing
        }

        protected override void MapFromStrings(string[] data){
            // do nothing
        }
    }
}