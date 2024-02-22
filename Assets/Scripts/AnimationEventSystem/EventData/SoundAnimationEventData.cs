namespace Project.AnimationEventSystem
{
    public class SoundAnimationEventData : AnimationEventData
    {
        public int SoundId { get; private set; }
        public float Volume { get; private set; }

        public SoundAnimationEventData() : this(0, 1f) { }

        protected SoundAnimationEventData(int soundId, float volume){
            SoundId = soundId;
            Volume = volume;
        }

        public override AnimationEventData Clone()
        {
            return new SoundAnimationEventData(SoundId, Volume);
        }

        public override void MapFromOther(AnimationEventData other)
        {
            if(other is SoundAnimationEventData soundAnimationEventData){
                SoundId = soundAnimationEventData.SoundId;
                Volume = soundAnimationEventData.Volume;
            }
        }

        protected override void MapFromStrings(string[] data)
        {
            SoundId = int.Parse(data[0]);
            Volume = float.Parse(data[1]);
        }
    }
}