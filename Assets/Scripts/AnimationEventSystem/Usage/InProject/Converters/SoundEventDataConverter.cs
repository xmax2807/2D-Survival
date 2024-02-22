namespace Project.AnimationEventSystem.UsageInProject
{
    public class SoundEventDataConverter : IEventDataConverter
    {
        public object Convert(AnimationEventData data)
        {
            if(data is SoundAnimationEventData soundEventData){
                return new Project.GameEventSystem.SoundEventData(soundEventData.SoundId, soundEventData.Volume);
            }
            throw new System.NotImplementedException("data is not SoundAnimationEventData");
        }
    }
}